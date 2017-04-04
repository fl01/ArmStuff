using System;
using Microsoft.Extensions.Configuration;

namespace Jasmine.Api.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfigurationRoot _configuration;

        public SettingsService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public Guid AuthCode
        {
            get
            {
                return _configuration.GetValue<Guid>("Auth:AuthCode");
            }
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("MongoDb");
        }

        public string GetMovementsDbName()
        {
            return _configuration.GetConnectionString("MongoDbName");
        }

        public TimeSpan GetSensorActivityExpiry()
        {
            return _configuration.GetValue<TimeSpan>("SensorActivity:ExpiryTime", TimeSpan.FromSeconds(15));
        }

        public TimeSpan GetWatcherDelay()
        {
            return _configuration.GetValue<TimeSpan>("StatusWatcher:WatchDelay", TimeSpan.FromSeconds(1));
        }
    }
}
