using System;
using Jasmine.Api.Models;

namespace Jasmine.Api.Extensions
{
    public static class SensorActivityExtensions
    {
        public static bool IsActive(this SensorActivity activity, TimeSpan expiration)
        {
            return activity != null && (DateTime.UtcNow - activity.EntryDate) <= expiration;
        }
    }
}
