using System;
using Jasmine.Api.Definitions;

namespace Jasmine.Api.Models
{
    public class DeviceStatus
    {
        public Device DeviceInfo { get; set; }

        public OccupationStatus Status { get; set; }

        public DateTime LastActivity { get; set; }
    }
}
