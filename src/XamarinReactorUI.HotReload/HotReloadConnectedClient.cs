//using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinReactorUI
{
    internal class HotReloadConnectedClient
    {
        private readonly HotReloadServer _server;
        private readonly TcpClient _connectedClient;

        public HotReloadConnectedClient(HotReloadServer server, TcpClient connectedClient)
        {
            _server = server;
            _connectedClient = connectedClient;
        }

        public void Run(CancellationToken cancellationToken)
        {
            var stream = _connectedClient.GetStream();
            var binaryReader = new BinaryReader(stream);

            try
            {
                int length = binaryReader.ReadInt32();
                if (length == -1)
                    return;

                var assemblyRaw = binaryReader.ReadBytes(length);

                _server.OnReceivedAssembly(assemblyRaw);
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }


        //public async void Run(CancellationToken cancellationToken)
        //{
        //    var stream = _connectedClient.GetStream();
        //    var factory = new WebSocketServerFactory();

        //    try
        //    {
        //        var context = await factory.ReadHttpHeaderFromStreamAsync(stream, cancellationToken);

        //        if (!context.IsWebSocketRequest)
        //        {
        //            _connectedClient.Close();
        //            return;
        //        }

        //        var webSocket = await factory.AcceptWebSocketAsync(context, cancellationToken);

        //        while (!cancellationToken.IsCancellationRequested)
        //        {
        //            int length = await ReceiveAssemblyRawLength(webSocket, cancellationToken);
        //            if (length == -1)
        //                return;

        //            var assemblyRaw = await ReceiveAssemblyRaw(webSocket, cancellationToken, length);

        //            _server.ReceivedAssebly(assemblyRaw);
        //        }
        //    }
        //    catch (OperationCanceledException)
        //    { 

        //    }
        //}

        //private async Task<int> ReceiveAssemblyRawLength(WebSocket webSocket, CancellationToken cancellationToken)
        //{
        //    var buffer = new ArraySegment<byte>(new byte[4]);
        //    var result = await webSocket.ReceiveAsync(buffer, cancellationToken);

        //    if (result.MessageType == WebSocketMessageType.Close)
        //        return -1;

        //    return BitConverter.ToInt32(buffer.Array, 0);
        //}

        //private async Task<byte[]> ReceiveAssemblyRaw(WebSocket webSocket, CancellationToken cancellationToken, int length)
        //{
        //    var buffer = new byte[length];
        //    int offset = 0;
        //    while (length > offset)
        //    {
        //        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, offset, length - offset), cancellationToken);
        //        switch (result.MessageType)
        //        {
        //            case WebSocketMessageType.Close:
        //                return null;
        //            case WebSocketMessageType.Text:
        //                throw new NotSupportedException();
        //            case WebSocketMessageType.Binary:
        //                length += result.Count;
        //                break;
        //        }
        //    }

        //    return buffer;
        //}
    }
}
