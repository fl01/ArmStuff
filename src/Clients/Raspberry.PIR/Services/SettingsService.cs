using System;
using Microsoft.Extensions.Configuration;

namespace Raspberry.PIR.Services
{
    public class SettingsService : ISettingsService
    {
        private Lazy<Guid> _deviceId = new Lazy<Guid>(Guid.Parse("BB4F1E20-6B52-4EA5-B120-AEFE21894B49"));
        private IConfigurationRoot _configuration;

        public SettingsService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("AppConfig.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public Guid DeviceId
        {
            get
            {
                return _deviceId.Value;
            }
        }

        public string MovementEndpointUrl
        {
            get
            {
                return _configuration.GetSection("Endpoints")["Movement"];
            }
        }

        public string PingEndpointUrl
        {
            get
            {
                return _configuration.GetSection("Endpoints")["Ping"];
            }
        }

        public int PirHeaderNum
        {
            get
            {
                string rawNum = _configuration.GetSection("GPIO")["PirPinHeaderNum"];
                int headerNum;
                if (!int.TryParse(rawNum, out headerNum))
                {
                    Console.WriteLine($"Invalid value of {nameof(PirHeaderNum)}");
                }

                return headerNum;
            }
        }
    }
}
