using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace domicats
{
    public class WebSocketMiddleware
    {
        // https://gunnarpeipman.com/aspnet/aspnet-core-websocket-chart/
        private readonly RequestDelegate _next;
        private readonly WebSocketManager _socketManager;

        public WebSocketMiddleware(RequestDelegate next, WebSocketManager socketManager)
        {
            _next = next;
            _socketManager = socketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var id = _socketManager.AddSocket(socket);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _socketManager.RemoveSocket(id);
                    return;
                }
                string data = GetTrimedString(buffer);
                _socketManager.ReceivedMessage(socket, data);

            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            //var buffer = new byte[1024 * 4];//////////////////////
            while (socket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024 * 4];
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }
        }

        private static string GetTrimedString(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);
            Array.Resize(ref array, lastIndex + 1);
            return Encoding.ASCII.GetString(array);
        }
    }
}
