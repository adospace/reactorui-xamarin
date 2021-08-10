using CommandLine;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinReactorUI.HotReloadConsole
{
    class Program
    {
        public class Options
        {
            [Option('a', "assembly", Required = true, HelpText = "Assembly file name to monitor and send to xamarin-reactorui hot reload server.")]
            public string AssemblyPath { get; set; }

            [Option('p', "port", Required = false, HelpText = "xamarin-reactorui hot reload server port mapped to emulator port.")]
            public int Port { get; set; } = 45820;

            [Option('m', "monitor", Required = false, HelpText = "Monitor assembly.")]
            public bool Monitor { get; set; } = true;
        }

        private static int _remoteServerPort;
        private static DateTime _lastWriteTime;

        static int Main(string[] args)
        {
            //C:\Program Files (x86)\Android\android-sdk>adb forward tcp:45820 tcp:45821
            if (!ExecutePortForwardCommmand())
                return -1;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       _remoteServerPort = o.Port;

                       SendAssemblyToEmulatorAsync(o.AssemblyPath).Wait();

                       if (o.Monitor)
                       {
                           Monitor(o.AssemblyPath);
                       }
                       else
                       { 
                       }
                   });

            return 0;
        }

        private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private static bool ExecutePortForwardCommmand()
        {
            var adbCommandLine = "\"" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Android", "sdk", "platform-tools", "adb" + (IsWindows ? ".exe" : "")) + "\" "
                + "forward tcp:45820 tcp:45820";
            var adbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Android", "android-sdk", "platform-tools", "adb.exe");

            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "forward tcp:45820 tcp:45820";
            process.StartInfo.FileName = adbPath;

            try
            {
                process.Start();

                var adb_output = process.StandardOutput.ReadToEnd();

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator (executing '{adbPath} forward tcp:45820 tcp:45820' adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        private static void Monitor(string assemblyPath)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Console.WriteLine($"Start listening '{assemblyPath}'");

            // Create a new FileSystemWatcher and set its properties.
            using var watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(assemblyPath),

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                NotifyFilter = NotifyFilters.LastWrite,
                                //NotifyFilters.LastAccess
                                // | NotifyFilters.LastWrite
                                // | NotifyFilters.FileName
                                // | NotifyFilters.DirectoryName,

                // Only watch text files.
                Filter = Path.GetFileName(assemblyPath) //"*.txt"
            };

            // Add event handlers.
            watcher.Changed += OnChanged;
            //watcher.Created += OnChanged;
            //watcher.Deleted += OnChanged;
            //watcher.Renamed += OnRenamed;

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press Cancel Key to quit");
            //while (Console.Read() != 'q') ;

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            cancellationTokenSource.Token.WaitHandle.WaitOne();

            cancellationTokenSource.Cancel();
        }

        private static bool _sending = false;
        private static async void OnChanged(object sender, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime == _lastWriteTime)
                return;

            if (_sending)
                return;

            _lastWriteTime = lastWriteTime;
            _sending = true;

            await SendAssemblyToEmulatorAsync(e.FullPath);

            _sending = false;
        }

        private static async Task<bool> SendAssemblyToEmulatorAsync(string assemblyPath, bool debugging = true)
        {
            
            //ThreadHelper.ThrowIfNotOnUIThread();

            //outputPane.OutputString($"Sending to emulator new assembly (debugging={debugging})...");
            //outputPane.Activate(); // Brings this pane into view

            var client = new TcpClient
            {
                ReceiveTimeout = 15000,
                SendTimeout = 15000
            };

            try
            {
               
                await client.ConnectAsync(IPAddress.Loopback, 45820);

                var assemblyRaw = await FileUtil.ReadAllFileAsync(assemblyPath);

                var networkStream = client.GetStream();

                var lengthBytes = BitConverter.GetBytes(assemblyRaw.Length);
                await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                await networkStream.WriteAsync(assemblyRaw, 0, assemblyRaw.Length);

                await networkStream.FlushAsync();

                var assemblySymbolStorePath = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + ".pdb");

                if (File.Exists(assemblySymbolStorePath) && debugging)
                {
                    var assemblySynmbolStoreRaw = await FileUtil.ReadAllFileAsync(assemblySymbolStorePath);

                    lengthBytes = BitConverter.GetBytes(assemblySynmbolStoreRaw.Length);

                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                    await networkStream.WriteAsync(assemblySynmbolStoreRaw, 0, assemblySynmbolStoreRaw.Length);

                    await networkStream.FlushAsync();
                }
                else
                {
                    lengthBytes = BitConverter.GetBytes(0);

                    await networkStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);

                    await networkStream.FlushAsync();
                }

                var booleanBuffer = new byte[1];
                if (await networkStream.ReadAsync(booleanBuffer, 0, 1) == 0)
                    throw new SocketException();

                Console.WriteLine($"Sent new assembly ({assemblyRaw.Length} bytes) to emulator{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"
Unable to connect to ReactorUI Hot Reload module
Please ensure that:
1) Only one device is running among emulators and physical devices
2) Application is running either in debug or release mode
3) RxApplication call WithHotReload()
Socket exception: {ex.Message}
");
                return false;
            }
            finally
            {
                client.Close();
            }

            return true;
        }


        //private static async Task SendAssemblyToEmulator(string assemblyPath)
        //{
        //    var client = new TcpClient();

        //    try
        //    {
        //        Console.WriteLine($"File changed, sending new assembly to emulator");

        //        await client.ConnectAsync(IPAddress.Loopback, _remoteServerPort);

        //        var assemblyRaw = await File.ReadAllBytesAsync(assemblyPath);

        //        var binaryWriter = new BinaryWriter(client.GetStream());

        //        binaryWriter.Write(assemblyRaw.Length);

        //        binaryWriter.Write(assemblyRaw);

        //        binaryWriter.Flush();

        //        var binaryReader = new BinaryReader(client.GetStream());

        //        binaryReader.ReadBoolean();

        //        Console.WriteLine($"File sent");
        //    }
        //    catch (OperationCanceledException)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        client.Close();
        //    }
        //}


        //private static async void OnChanged(object sender, FileSystemEventArgs e)
        //{
        //    DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
        //    if (lastWriteTime == _lastWriteTime)
        //        return;

        //    _lastWriteTime = lastWriteTime;

        //    var factory = new WebSocketClientFactory();
        //    try
        //    {
        //        Console.WriteLine($"File changed, sending new assembly to emulator");

        //        var webSocket = await factory.ConnectAsync(new Uri($"wss://localhost:{_remoteServerPort}"), _cancellationToken);

        //        var assemblyRaw = await File.ReadAllBytesAsync(e.FullPath);

        //        await webSocket.SendAsync(new ArraySegment<byte>(BitConverter.GetBytes(assemblyRaw.Length)), System.Net.WebSockets.WebSocketMessageType.Binary, true, _cancellationToken);

        //        await webSocket.SendAsync(new ArraySegment<byte>(assemblyRaw), System.Net.WebSockets.WebSocketMessageType.Binary, true, _cancellationToken);

        //        await webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, null, _cancellationToken);

        //        Console.WriteLine($"File sent");
        //    }
        //    catch (OperationCanceledException)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}
    }
}
