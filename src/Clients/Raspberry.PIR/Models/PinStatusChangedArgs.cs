namespace Raspberry.PIR.Models
{
    public class PinStatusChangedArgs
    {
        public SensorType Sensor { get; set; }

        public bool IsActive { get; private set; }

        public PinStatusChangedArgs(SensorType sensor, bool isActive)
        {
            Sensor = sensor;
            IsActive = isActive;
        }
    }
}