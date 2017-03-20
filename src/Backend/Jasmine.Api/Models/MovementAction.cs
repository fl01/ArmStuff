using System;

namespace Jasmine.Api.Models
{
    public class MovementAction
    {
        public Guid Id { get; set; }

        public Guid DeviceId { get; set; }

        public int Value { get; set; }

        public string Sensor { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
