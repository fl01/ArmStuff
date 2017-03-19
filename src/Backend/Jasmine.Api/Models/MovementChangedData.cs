using System;

namespace Jasmine.Api.Models
{
    public class MovementChangedData
    {
        public Guid DeviceId { get; set; }

        public int Value { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
