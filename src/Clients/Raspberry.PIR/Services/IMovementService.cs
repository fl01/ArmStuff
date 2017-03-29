using Raspberry.PIR.Models;
using Raspberry.PIR.Services.GPIO;

namespace Raspberry.PIR.Services
{
    public interface IMovementService
    {
        void SetSensor(SensorType sensor, IPinService pinService);

        void Initialize();
    }
}
