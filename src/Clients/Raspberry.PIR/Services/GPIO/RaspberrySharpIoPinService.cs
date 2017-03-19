using System;
using Raspberry.IO.GeneralPurpose;
using Raspberry.PIR.Models;

namespace Raspberry.PIR.Services.GPIO
{
    public class RaspberrySharpIoPinService : IPinService
    {
        private bool _isStatusWatchRunning = false;
        private GpioConnection _pinConnection = null;

        public event EventHandler<PinStatusChangedArgs> OnStatusChanged;

        public void BeginStatusWatch()
        {
            if (_isStatusWatchRunning)
            {
                return;
            }

            _isStatusWatchRunning = true;
            _pinConnection.PinStatusChanged += (s, args) => OnStateChanged(args);
        }

        public void SetPinUsingHeaderNumber(int header)
        {
            if (_pinConnection != null)
            {
                (_pinConnection as IDisposable)?.Dispose();
                _pinConnection = null;
            }

            if (!Enum.IsDefined(typeof(ConnectorPin), header))
            {
                throw new ArgumentException("Invalid header number");
            }

            var pin = (ConnectorPin)header;
            _pinConnection = new GpioConnection(pin.Input());

            Console.WriteLine("PinConnection has been set");
        }

        private void OnStateChanged(PinStatusEventArgs statusArgs)
        {
            Console.WriteLine("State has been changed to " + statusArgs.Enabled);

            OnStatusChanged?.Invoke(this, new PinStatusChangedArgs(statusArgs.Enabled.ToInt32()));
        }
    }
}
