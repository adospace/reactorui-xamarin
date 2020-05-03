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
using MonoDevelop.Core;
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
            var now = DateTime.Now;
            if (await UpdateInternal())
            {
                LoggingService.LogDebug($"ReactorUI hot reloaded completed in {(DateTime.Now - now).TotalMilliseconds}ms");
            }

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
                LoggingService.LogWarning("Solution doesn't contain a Xamarin Forms project referencing BOTH XamarinReactorUI and XamarinReactorUI.HotReload");
                return false;
            }

            var startupProject = solution.StartupItem as DotNetProject;

            if (startupProject == null)
            {
                LoggingService.LogWarning("Solution hasn't a valid startup project");
                return false;
            }

            OS os = OS.None;
            if (startupProject.TargetFramework.Id.Identifier == "Xamarin.iOS")
            {
                os = OS.iOS;
            }
            else if (startupProject.TargetFramework.Id.Identifier == "MonoAndroid")
            {
                os = OS.Android;
            }

            if (os == OS.None)
            {
                LoggingService.LogWarning($"Unable to find an valid Xamarin Android or iOS project in the solution");
                return false;
            }

            IdeApp.Workbench.SaveAll();

            var buildResult = await reactorUIProject.Build(IdeApp.Workbench.ProgressMonitors.GetBuildProgressMonitor(), ConfigurationSelector.Default);

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

        private async Task<bool> IsHotReloadAvailable()
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
            else if (startupProject.TargetFramework.Id.Identifier == "MonoAndroid")
            {
                os = OS.Android;
            }

            if (os == OS.None)
            {
                return false;
            }

            return true;
        }
    

        protected override async void Update(CommandInfo info)
        {
            info.Enabled = await IsHotReloadAvailable();

            base.Update(info);
        }

        private static bool ExecutePortForwardCommmand()        {            var adbCommandLine = "adb forward tcp:45820 tcp:45820";            var process = new System.Diagnostics.Process();            process.StartInfo.CreateNoWindow = true;            process.StartInfo.RedirectStandardOutput = true;            process.StartInfo.RedirectStandardInput = true;            process.StartInfo.RedirectStandardError = true;            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = string.Format("-c \"{0}\"", adbCommandLine);
            process.StartInfo.FileName = "/bin/bash";            try            {                process.Start();                var adb_output = process.StandardOutput.ReadToEnd();                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)                    throw new InvalidOperationException($"Unable to forward tcp port from emulator, is emulator running? (adb tool returned '{adb_output}')");            }            catch (Exception ex)            {                LoggingService.LogError($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}{process.StandardError.ReadToEnd()}{Environment.NewLine}{ex}");
                //outputPane.OutputString($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}");
                //outputPane.OutputString($"{process.StandardError.ReadToEnd()}{Environment.NewLine}");
                //outputPane.OutputString($"{ex}{Environment.NewLine}");
                return false;            }            return true;        }

        private static async Task<bool> SendAssemblyToEmulatorAsync(string assemblyPath, bool debugging)        {
            var client = new TcpClient            {                ReceiveTimeout = 15000,                SendTimeout = 15000            };            try            {                await client.ConnectAsync(IPAddress.Loopback, 45820);                var assemblyRaw = await FileUtil.ReadAllFileAsync(assemblyPath);                var networkStream = client.GetStream();                var lengthBytes = BitConverter.GetBytes(assemblyRaw.Length);                await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                await networkStream.WriteAsync(assemblyRaw, 0, assemblyRaw.Length);                await networkStream.FlushAsync();                var assemblySymbolStorePath = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb");                if (File.Exists(assemblySymbolStorePath) && debugging)                {                    var assemblySynmbolStoreRaw = await FileUtil.ReadAllFileAsync(assemblySymbolStorePath);                    lengthBytes = BitConverter.GetBytes(assemblySynmbolStoreRaw.Length);                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);                    await networkStream.WriteAsync(assemblySynmbolStoreRaw, 0, assemblySynmbolStoreRaw.Length);                    await networkStream.FlushAsync();                }                else                {                    lengthBytes = BitConverter.GetBytes(0);                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);                    await networkStream.FlushAsync();                }                var booleanBuffer = new byte[1];                if (await networkStream.ReadAsync(booleanBuffer, 0, 1) == 0)                    throw new SocketException();                LoggingService.LogDebug($"Sent new assembly ({assemblyRaw.Length} bytes) to emulator{Environment.NewLine}");            }            catch (Exception ex)            {                LoggingService.LogError($@"Unable to connect to ReactorUI Hot Reload modulePlease ensure that:1) Only one device is running among emulators and physical devices2) Application is running either in debug or release mode3) RxApplication call WithHotReload()Socket exception: {ex.Message}");                return false;            }            finally            {                client.Close();            }            return true;        }
    }
}
