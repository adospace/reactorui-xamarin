using CommandLine;
using Ninja.WebSockets;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
        }

        private static int _remoteServerPort;
        private static CancellationToken _cancellationToken;
        private static FileSystemWatcher _watcher;
        private static DateTime _lastWriteTime;

        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = cancellationTokenSource.Token;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       _remoteServerPort = o.Port;
                       StartMonitoring(o.AssemblyPath);
                       Console.WriteLine($"Start listening '{o.AssemblyPath}'");
                   });

            // Wait for the user to quit the program.
            Console.WriteLine("Press 'q' to quit the sample.");
            while (Console.Read() != 'q') ;

            cancellationTokenSource.Cancel();

            _watcher?.Dispose();
        }

        private static void StartMonitoring(string assemblyPath)
        {
            // Create a new FileSystemWatcher and set its properties.
            _watcher = new FileSystemWatcher
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
            _watcher.Changed += OnChanged;
            //watcher.Created += OnChanged;
            //watcher.Deleted += OnChanged;
            //watcher.Renamed += OnRenamed;

            // Begin watching.
            _watcher.EnableRaisingEvents = true;

        }

        private static async void OnChanged(object sender, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime == _lastWriteTime)
                return;

            _lastWriteTime = lastWriteTime;

            var client = new TcpClient();

            try
            {
                Console.WriteLine($"File changed, sending new assembly to emulator");

                await client.ConnectAsync(IPAddress.Loopback, _remoteServerPort);

                var assemblyRaw = await File.ReadAllBytesAsync(e.FullPath);

                var binaryWriter = new BinaryWriter(client.GetStream());

                binaryWriter.Write(assemblyRaw.Length);

                binaryWriter.Write(assemblyRaw);

                Console.WriteLine($"File sent");
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                client.Close();
            }
        }


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
