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

        public MovementService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _sensors = new Dictionary<SensorType, IPinService>();
        }

        public void Initialize()
        {
            InitalizePirSensor(_sensors[SensorType.PIR]);
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

            sensorService.OnInputStatusChanged += (s, e) => OnPirStatusChanged(e);
            sensorService.ConnectInput();
        }

        private void OnPirStatusChanged(PinStatusChangedArgs e)
        {
            PushPirDataToServer(e);
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
    }
}
