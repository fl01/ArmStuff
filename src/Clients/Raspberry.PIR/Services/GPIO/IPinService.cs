using System;
using Raspberry.PIR.Models;

namespace Raspberry.PIR.Services.GPIO
{
    public interface IPinService
    {
        event EventHandler<PinStatusChangedArgs> OnStatusChanged;

        void SetInputPinUsingHeaderNumber(int header);

        void SetOutputPinUsingHeaderNumber(int header);

        void ConnectInput();

        void Toggle();

        void ConnectOutput();
    }
}
