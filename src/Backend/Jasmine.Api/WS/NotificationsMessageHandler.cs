using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Jasmine.Api.WS
{
    public class NotificationsMessageHandler : WebSocketHandler, IWebSocketHandler
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
