using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinReactorUI.HotReloadServerConsole
{
    internal class HotReloadConnectedClient
    {
        private readonly TcpClient _connectedClient;

        public HotReloadConnectedClient(TcpClient connectedClient)
        {
            _connectedClient = connectedClient;
        }

        public async void Run(CancellationToken cancellationToken)
        {
            var stream = _connectedClient.GetStream();
            var factory = new WebSocketServerFactory();

            try
            {
                var context = await factory.ReadHttpHeaderFromStreamAsync(stream, cancellationToken);

                if (!context.IsWebSocketRequest)
                {
                    _connectedClient.Close();
                    return;
                }

                var webSocket = await factory.AcceptWebSocketAsync(context, cancellationToken);

                await Receive(webSocket, cancellationToken);
            }
            catch (OperationCanceledException)
            { 
            
            }
        }

        private async Task Receive(WebSocket webSocket, CancellationToken cancellationToken)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                switch (result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        return;
                    case WebSocketMessageType.Text:
                    case WebSocketMessageType.Binary:
                        string value = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        Console.WriteLine(value);
                        break;
                }
            }
        }
    }
}
