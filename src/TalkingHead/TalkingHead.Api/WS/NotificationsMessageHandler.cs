using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace TalkingHead.Api.WS
{
    public class NotificationsMessageHandler : WebSocketHandlerBase, IWebSocketHandler
    {
        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager)
            : base(webSocketConnectionManager)
        {
        }

        public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new NotSupportedException();
        }
    }
}
