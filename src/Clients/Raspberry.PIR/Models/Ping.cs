using System;

namespace Raspberry.PIR.Models
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
