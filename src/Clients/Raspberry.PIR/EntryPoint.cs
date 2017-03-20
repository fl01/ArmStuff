using System;
using Raspberry.PIR.Http;
using Raspberry.PIR.Models;
using Raspberry.PIR.Services;
using Raspberry.PIR.Services.GPIO;

namespace Raspberry.PIR
{
    public class EntryPoint
    {
        private static ISettingsService _settingsService;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hey. I'm PIR handler.");

            IPinService pinService = new RaspberrySharpIoPinService("PIR");
            _settingsService = new SettingsService();
            Console.WriteLine($"Device Id: {_settingsService.DeviceId}");

            pinService.OnStatusChanged += (s, e) => OnMovementChanged(e);
            pinService.SetPinUsingHeaderNumber(_settingsService.PirHeaderNum);
            pinService.BeginStatusWatch();

            Console.WriteLine("Initialized");
            Console.ReadLine();
            Console.WriteLine("PIR handler is out");
        }

        private static async void OnMovementChanged(PinStatusChangedArgs args)
        {
            var movementData = new MovementChangedData(_settingsService.DeviceId, args.Sensor, args.Value);
            Console.WriteLine($"Movement data will be sent to {_settingsService.MovementEndpointUrl}");

            using (IHttpClient httpClient = new SimpleHttpClient())
            {
                await httpClient.PostJsonAsync(_settingsService.MovementEndpointUrl, movementData);
                Console.WriteLine("Done");
            }
        }
    }
}