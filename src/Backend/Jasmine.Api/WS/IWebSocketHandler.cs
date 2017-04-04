using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Jasmine.Api.WS
{
    public interface IWebSocketHandler
    {
        Task SendMessageToAllAsync(string message);

        Task OnConnected(WebSocket socket);

        Task OnDisconnected(WebSocket socket);

        Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
