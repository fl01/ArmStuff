using System;
using Raspberry.PIR.Models;
using Raspberry.PIR.Services;
using Raspberry.PIR.Services.GPIO;

namespace Raspberry.PIR
{
    public class EntryPoint
    {
        private static ISettingsService _settingsService;
        private static IMovementService _movementService;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hey. I'm Movement detection service.");

            _settingsService = new SettingsService();
            _movementService = new MovementService(_settingsService);

            IPinService pirSensor = new RaspberrySharpIoPinService(_settingsService, SensorType.PIR);
            pirSensor.SetInputPinUsingHeaderNumber(_settingsService.PirHeaderNum);
            _movementService.SetSensor(SensorType.PIR, pirSensor);

            IPinService rangeSensor = new RaspberrySharpIoPinService(_settingsService, SensorType.Range);
            rangeSensor.SetInputPinUsingHeaderNumber(_settingsService.RangeEchoHeaderNum);
            rangeSensor.SetOutputPinUsingHeaderNumber(_settingsService.RangeTriggerHeaderNum);
            _movementService.SetSensor(SensorType.Range, rangeSensor);

            _movementService.Initialize();
            Console.WriteLine("Initialized");

            Console.WriteLine($"Device Id: {_settingsService.DeviceId}");
            Console.ReadLine();
            Console.WriteLine("Movement detection is out");
        }
    }
}