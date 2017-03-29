using System;
using System.Collections.Generic;
using Raspberry.PIR.Http;
using Raspberry.PIR.Models;
using Raspberry.PIR.Services.GPIO;

namespace Raspberry.PIR.Services
{
    public class MovementService : IMovementService
    {
        private Dictionary<SensorType, IPinService> _sensors;
        private readonly ISettingsService _settingsService;
        private double rangeEchoBegin;

        public MovementService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _sensors = new Dictionary<SensorType, IPinService>();
        }

        public void Initialize()
        {
            InitalizePirSensor(_sensors[SensorType.PIR]);
            InitalizeRangeSensor(_sensors[SensorType.Range]);
        }

        public void SetSensor(SensorType sensor, IPinService pinService)
        {
            _sensors[sensor] = null;
            _sensors[sensor] = pinService;
        }

        private void InitalizePirSensor(IPinService sensorService)
        {
            if (sensorService == null)
            {
                Console.WriteLine("WARNING: Missing PIR sensor service");
                return;
            }

            sensorService.OnStatusChanged += (s, e) => OnPirStatusChanged(e);
            sensorService.ConnectInput();
        }

        private void OnPirStatusChanged(PinStatusChangedArgs e)
        {
            // TODO : uncomment once range sensor is fixed
            //PushPirDataToServer(e);

            if (e.IsActive)
            {
                Console.WriteLine($"Toggle begin");
                _sensors[SensorType.Range].Toggle();
                Console.WriteLine($"Toggle end");
            }
        }

        private async void PushPirDataToServer(PinStatusChangedArgs args)
        {
            if (args.IsActive || _settingsService.PushFalseData)
            {
                var movementData = new MovementChangedData(_settingsService.DeviceId, args.Sensor, args.IsActive.ToInt32());
                Console.WriteLine($"Movement data will be sent to {_settingsService.MovementEndpointUrl}");

                using (IHttpClient httpClient = new SimpleHttpClient())
                {
                    await httpClient.PostJsonAsync(_settingsService.MovementEndpointUrl, movementData);
                    Console.WriteLine("Done");
                }
            }
        }

        private void InitalizeRangeSensor(IPinService sensorService)
        {
            if (sensorService == null)
            {
                Console.WriteLine("WARNING: Missing Range sensor service");
                return;
            }

            sensorService.OnStatusChanged += (s, e) => OnRangeEchoStatusChanged(e);
            sensorService.ConnectOutput();
            sensorService.ConnectInput();
            Console.WriteLine("Range initialized");
        }

        private void OnRangeEchoStatusChanged(PinStatusChangedArgs e)
        {
            if (e.IsActive)
            {
                rangeEchoBegin = GetUnixTimeStamp();
            }
            else
            {
                double distance = Math.Round((GetUnixTimeStamp() - rangeEchoBegin) * 17150, 2);
                Console.WriteLine("Distance = " + distance);
            }

            Console.WriteLine(DateTime.UtcNow + " Range Echo Status - " + e.IsActive);
        }

        private double GetUnixTimeStamp()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
