using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinReactorUI.HotReloadServerConsole
{
    internal class HotReloadServer
    {
        private readonly int _listenPort;

        public HotReloadServer(int listenPort)
        {
            _listenPort = listenPort;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            var serverSocket = new TcpListener(IPAddress.Any, _listenPort);

            serverSocket.Start();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (serverSocket.Pending())
                        break;

                    await Task.Delay(100, cancellationToken);
                }

                var connectedClient = await serverSocket.AcceptTcpClientAsync();

                new HotReloadConnectedClient(connectedClient)
                    .Run(cancellationToken);
            }
            catch (TaskCanceledException)
            { 
            
            }
        }
    }
}
