using System;
using Raspberry.PIR.Models;

namespace Raspberry.PIR.Services.GPIO
{
    public interface IPinService
    {
        event EventHandler<PinStatusChangedArgs> OnInputStatusChanged;

        void SetInputPinUsingHeaderNumber(int header);

        void ConnectInput();
    }
}
