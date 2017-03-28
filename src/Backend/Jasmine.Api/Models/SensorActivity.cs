using System;
using Jasmine.Api.Definitions;

namespace Jasmine.Api.Models
{
    public class SensorActivity
    {
        public DateTime EntryDate { get; set; }

        public int Value { get; set; }

        public SensorType Type { get; set; }
    }
}
