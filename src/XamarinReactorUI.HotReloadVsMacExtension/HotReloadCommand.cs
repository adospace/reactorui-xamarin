using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Mono.Addins;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace XamarinReactorUI.HotReloadVsMacExtension
{
    public class HotReloadCommandHandler : CommandHandler
    {
        private enum OS
        {
            None,

            Android,

            iOS
        }

        protected override async void Run()
        {

            await UpdateInternal();

            base.Run();
        }

        private async Task<bool> UpdateInternal()
        {
            var solution = IdeApp.Workspace.CurrentSelectedSolution;

            if (solution == null)
            {
                return false;
            }

            DotNetProject reactorUIProject = null;

            foreach (var project in solution.GetAllProjects()
                .OfType<DotNetProject>()
                .Where(_ => _.TargetFramework.Id.Identifier == ".NETStandard"))
            {
                var packages = await project.GetPackageDependencies(ConfigurationSelector.Default, CancellationToken.None);
                if (packages.Any(_ => _.Name == "XamarinReactorUI") && packages.Any(_ => _.Name == "XamarinReactorUI.HotReload"))
                {
                    reactorUIProject = project;
                    break;
                }
            }

            if (reactorUIProject == null)
            {
                return false;
            }

            var startupProject = solution.StartupItem as DotNetProject;

            if (startupProject == null)
            {
                return false;
            }

            OS os = OS.None;
            if (startupProject.TargetFramework.Id.Identifier == "Xamarin.iOS")
            {
                os = OS.iOS;
            }
            else if (startupProject.TargetFramework.Id.Identifier == "Android")
            {
                os = OS.Android;
            }

            if (os == OS.None)
            {
                return false;
            }

            IdeApp.Workbench.SaveAll();

            var buildResult = await reactorUIProject.Build(new MonoDevelop.Core.ProgressMonitor(), ConfigurationSelector.Default);

            if (buildResult.Failed)
            {

                return false;
            }

            if (os == OS.Android)
            {
                if (!ExecutePortForwardCommmand())
                {
                    return false;
                }
            }

            if (!await SendAssemblyToEmulatorAsync(reactorUIProject.GetOutputFileName(ConfigurationSelector.Default).FullPath, true))
            {

                return false;
            }

            return true;

        }


        protected override void Update(CommandInfo info)
        {
            base.Update(info);
        }

        private static bool ExecutePortForwardCommmand()        {            //ThreadHelper.ThrowIfNotOnUIThread();
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var adbCommandLine = "\"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Android", "sdk", "platform-tools", "adb" + (isWindows ? ".exe" : "")) + "\" "                + "forward tcp:45820 tcp:45820";            var process = new System.Diagnostics.Process();            process.StartInfo.CreateNoWindow = true;            process.StartInfo.RedirectStandardOutput = true;            process.StartInfo.RedirectStandardInput = true;            process.StartInfo.RedirectStandardError = true;            process.StartInfo.UseShellExecute = false;            if (isWindows)            {                process.StartInfo.Arguments = adbCommandLine;                process.StartInfo.FileName = "powershell";            }            else            {                process.StartInfo.Arguments = string.Format("-c \"{0}\"", adbCommandLine);                process.StartInfo.FileName = "/bin/sh";            }            try            {                process.Start();                var adb_output = process.StandardOutput.ReadToEnd();                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)                    throw new InvalidOperationException($"Unable to forward tcp port from emulator, is emulator running? (adb tool returned '{adb_output}')");            }            catch (Exception ex)            {                //outputPane.OutputString($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}");                //outputPane.OutputString($"{process.StandardError.ReadToEnd()}{Environment.NewLine}");                //outputPane.OutputString($"{ex}{Environment.NewLine}");                return false;            }            return true;        }

        private static async Task<bool> SendAssemblyToEmulatorAsync(string assemblyPath, bool debugging)        {
            var client = new TcpClient            {                ReceiveTimeout = 15000,                SendTimeout = 15000            };            try            {                await client.ConnectAsync(IPAddress.Loopback, 45820);                var assemblyRaw = await FileUtil.ReadAllFileAsync(assemblyPath);                var networkStream = client.GetStream();                var lengthBytes = BitConverter.GetBytes(assemblyRaw.Length);                await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                await networkStream.WriteAsync(assemblyRaw, 0, assemblyRaw.Length);                await networkStream.FlushAsync();                var assemblySymbolStorePath = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb");                if (File.Exists(assemblySymbolStorePath) && debugging)                {                    var assemblySynmbolStoreRaw = await FileUtil.ReadAllFileAsync(assemblySymbolStorePath);                    lengthBytes = BitConverter.GetBytes(assemblySynmbolStoreRaw.Length);                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);                    await networkStream.WriteAsync(assemblySynmbolStoreRaw, 0, assemblySynmbolStoreRaw.Length);                    await networkStream.FlushAsync();                }                else                {                    lengthBytes = BitConverter.GetBytes(0);                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);                    await networkStream.FlushAsync();                }                var booleanBuffer = new byte[1];                if (await networkStream.ReadAsync(booleanBuffer, 0, 1) == 0)                    throw new SocketException();                //outputPane.OutputStringThreadSafe($"Sent new assembly ({assemblyRaw.Length} bytes) to emulator{Environment.NewLine}");            }            catch (Exception ex)            {                /*                outputPane.OutputStringThreadSafe($@"Unable to connect to ReactorUI Hot Reload modulePlease ensure that:1) Only one device is running among emulators and physical devices2) Application is running either in debug or release mode3) RxApplication call WithHotReload()Socket exception: {ex.Message}");*/                return false;            }            finally            {                client.Close();            }            return true;        }
    }
}
