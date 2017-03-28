using System;
using System.Collections.Generic;
using Jasmine.Api.Models;

namespace Jasmine.Api.Definitions
{
    public static class Devices
    {
        private static List<Device> _devices;

        public static IReadOnlyList<Device> All
        {
            get
            {
                return _devices.AsReadOnly();
            }
        }

        static Devices()
        {
            _devices = new List<Device>()
            {
                RUWC
            };
        }
        public static Device RUWC => new Device(new Guid("BB4F1E20-6B52-4EA5-B120-AEFE21894B49"), "RU WC");
    }
}
