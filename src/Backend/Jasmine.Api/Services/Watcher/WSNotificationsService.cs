using Jasmine.Api.Models;
using Jasmine.Api.WS;
using Newtonsoft.Json;

namespace Jasmine.Api.Services
{
    public class WSNotificationsService : INotificationsService
    {
        private readonly IWebSocketHandler _wsHandler;

        public WSNotificationsService(IWebSocketHandler wsHandler)
        {
            _wsHandler = wsHandler;
        }

        public async void OnDeviceStatusChanged(DeviceStatus deviceStatus)
        {
            if (deviceStatus == null)
            {
                return;
            }

            await _wsHandler.SendMessageToAllAsync(JsonConvert.SerializeObject(deviceStatus));
        }
    }
}
