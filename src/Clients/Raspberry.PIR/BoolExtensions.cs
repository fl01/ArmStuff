namespace Raspberry.PIR
{
    public static class BoolExtensions
    {
        public static int ToInt32(this bool value)
        {
            return value ? 1 : 0;
        }
    }
}
