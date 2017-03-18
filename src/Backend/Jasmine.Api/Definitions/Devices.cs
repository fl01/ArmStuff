using System;
using Jasmine.Api.Models;

namespace Jasmine.Api.Definitions
{
    public static class Devices
    {
        public static Device MotionSensor => new Device(new Guid("BB4F1E20-6B52-4EA5-B120-AEFE21894B49"), "WC");
    }
}
