using System;
using Raspberry.PIR.Models;

namespace Raspberry.PIR.Services.GPIO
{
    public interface IPinService
    {
        event EventHandler<PinStatusChangedArgs> OnStatusChanged;

        void SetPinUsingHeaderNumber(int header);

        void BeginStatusWatch();
    }
}
