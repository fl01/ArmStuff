namespace Raspberry.PIR.Models
{
    public class PinStatusChangedArgs
    {
        public string Sensor { get; set; }

        public int Value { get; private set; }

        public PinStatusChangedArgs(string sensor, int value)
        {
            Sensor = sensor;
            Value = value;
        }
    }
}