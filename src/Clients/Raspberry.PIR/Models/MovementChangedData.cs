using System;

namespace Raspberry.PIR.Models
{
    public class MovementChangedData
    {
        public Guid DeviceId { get; private set; }

        public int Value { get; private set; }

        public DateTime EntryDate { get; private set; }

        public MovementChangedData(Guid deviceId, int value)
        {
            DeviceId = deviceId;
            Value = value;
            EntryDate = DateTime.UtcNow;
        }
    }
}
