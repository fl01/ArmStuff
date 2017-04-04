using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public interface INotificationsService
    {
        void OnDeviceStatusChanged(DeviceStatus deviceStatus);
    }
}
