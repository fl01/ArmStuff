using System;
using Jasmine.Api.Definitions;

namespace Jasmine.Api.Models
{
    public class MovementAction
    {
        public Guid Id { get; set; }

        public Guid DeviceId { get; set; }

        public int Value { get; set; }

        public SensorType Sensor { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
