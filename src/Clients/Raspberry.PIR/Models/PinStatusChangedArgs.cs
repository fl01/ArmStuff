namespace Raspberry.PIR.Models
{
    public class PinStatusChangedArgs
    {
        public int Value { get; private set; }

        public PinStatusChangedArgs(int value)
        {
            Value = value;
        }
    }
}