using System;

namespace Jasmine.Api.Models
{
    public class Device
    {
        public Guid ID { get; private set; }

        public string Description { get; set; }

        public Device(Guid id, string description = null)
        {
            ID = id;
            Description = description;
        }
    }
}
