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

namespace XamarinReactorUI
{
    internal class HotReloadServer
    {
        private readonly int _listenPort;
        private CancellationTokenSource _cancellationTokenSource;
        private TcpListener _serverSocket;

        public HotReloadServer(int listenPort)
        {
            _listenPort = listenPort;
        }

        public async void Run()
        {
            lock (this)
            {
                if (_cancellationTokenSource != null)
                    return;

                _cancellationTokenSource = new CancellationTokenSource();
            }

            var cancellationToken = _cancellationTokenSource.Token;
            _serverSocket = new TcpListener(IPAddress.Any, _listenPort);

            try
            {

                _serverSocket.Start();

                while (!cancellationToken.IsCancellationRequested)
                {
                    var connectedClient = await _serverSocket.AcceptTcpClientAsync();

                    new HotReloadConnectedClient(this, connectedClient)
                        .Run(cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            finally
            {
                _serverSocket.Stop();
                _serverSocket = null;

                lock (this)
                    _cancellationTokenSource = null;
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (_cancellationTokenSource == null)
                    return;

                if (!_cancellationTokenSource.IsCancellationRequested)
                    _cancellationTokenSource.Cancel();
            }

            _serverSocket?.Stop();
        }

        public event EventHandler<ReceivedAssemblyEventArgs> ReceivedAssembly;

        internal void OnReceivedAssembly(byte[] assemblyRaw)
        {
            ReceivedAssembly?.Invoke(this, new ReceivedAssemblyEventArgs(assemblyRaw));
        }
    }
}
