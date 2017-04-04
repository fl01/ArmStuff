using System;
using Jasmine.Api.Definitions;

namespace Jasmine.Api.Models
{
    public class DeviceStatus
    {
        public Device DeviceInfo { get; set; }

        public OccupationStatus Status { get; set; }

        public DateTime LastActivity { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as DeviceStatus;
            return other != null
                && other.DeviceInfo != null
                && DeviceInfo != null
                && other.DeviceInfo.ID.Equals(DeviceInfo.ID)
                && other.Status == Status
                && other.LastActivity == LastActivity;
        }

        public override int GetHashCode()
        {
            return DeviceInfo.ID.GetHashCode() + Status.GetHashCode();
        }
    }
}
