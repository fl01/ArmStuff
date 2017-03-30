using System;
using Raspberry.IO.GeneralPurpose;
using Raspberry.PIR.Models;

namespace Raspberry.PIR.Services.GPIO
{
    public class RaspberrySharpIoPinService : IPinService
    {
        private readonly SensorType _sensor;
        private readonly ISettingsService _settingsService;

        private ConnectorPin? _inputPin = null;
        private GpioConnection _inputConnection = null;

        public event EventHandler<PinStatusChangedArgs> OnInputStatusChanged;

        public RaspberrySharpIoPinService(ISettingsService settingsService, SensorType sensor)
        {
            _settingsService = settingsService;
            _sensor = sensor;
        }

        public void ConnectInput()
        {
            if (_inputPin == null)
            {
                Console.WriteLine("Invalid pin");
            }

            _inputConnection = new GpioConnection(_inputPin.GetValueOrDefault().Input());
            _inputConnection.PinStatusChanged += (s, args) => OnInputStateChanged(args);
            Console.WriteLine($"{_sensor} - {_inputPin} - initialized");
        }

        public void SetInputPinUsingHeaderNumber(int header)
        {
            if (!Enum.IsDefined(typeof(ConnectorPin), header))
            {
                throw new ArgumentException("Invalid header number");
            }

            _inputPin = (ConnectorPin)header;
        }

        private void OnInputStateChanged(PinStatusEventArgs statusArgs)
        {
            Console.WriteLine($"{_sensor} - state has been changed to " + statusArgs.Enabled);

            OnInputStatusChanged?.Invoke(this, new PinStatusChangedArgs(_sensor, statusArgs.Enabled));
        }
    }
}
