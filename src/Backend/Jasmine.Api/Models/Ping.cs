using System;

namespace Jasmine.Api.Models
{
    public class Ping
    {
        public Guid DeviceId { get; private set; }

        public Ping(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
