using System;
using Microsoft.Extensions.Configuration;

namespace Raspberry.PIR.Services
{
    public class SettingsService : ISettingsService
    {
        private Lazy<Guid> _deviceId;
        private IConfigurationRoot _configuration;

        public SettingsService()
        {
            _deviceId = new Lazy<Guid>(() => Guid.Parse(GetDeviceId()));

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

        public bool PushFalseData
        {
            get
            {
                string raw = _configuration.GetSection("Me")["PushFalseData"];
                if (!bool.TryParse(raw, out bool pushFalseData))
                {
                    Console.WriteLine($"Invalid value of {nameof(PushFalseData)}");
                }

                return pushFalseData;
            }
        }

        public int PirHeaderNum
        {
            get
            {
                string rawNum = _configuration.GetSection("GPIO")["PirPinHeaderNum"];
                if (!int.TryParse(rawNum, out int headerNum))
                {
                    Console.WriteLine($"Invalid value of {nameof(PirHeaderNum)}");
                }

                return headerNum;
            }
        }

        public int RangeEchoHeaderNum
        {
            get
            {
                string rawNum = _configuration.GetSection("GPIO")["RangeEchoHeaderNum"];
                if (!int.TryParse(rawNum, out int headerNum))
                {
                    Console.WriteLine($"Invalid value of {nameof(RangeEchoHeaderNum)}");
                }

                return headerNum;
            }
        }

        public int RangeTriggerHeaderNum
        {
            get
            {
                string rawNum = _configuration.GetSection("GPIO")["RangeTriggerHeaderNum"];
                if (!int.TryParse(rawNum, out int headerNum))
                {
                    Console.WriteLine($"Invalid value of {nameof(RangeTriggerHeaderNum)}");
                }

                return headerNum;
            }
        }

        private string GetDeviceId()
        {
            return _configuration.GetSection("Me")["DeviceId"];
        }
    }
}
