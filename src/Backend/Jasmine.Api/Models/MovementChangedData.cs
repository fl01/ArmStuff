using System;
using System.ComponentModel.DataAnnotations;
using Jasmine.Api.Definitions;

namespace Jasmine.Api.Models
{
    public class MovementChangedData
    {
        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public SensorType Sensor { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }
    }
}
