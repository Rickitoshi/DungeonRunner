namespace Signals
{
    public class CoinsCountChangeSignal
    {
        public readonly int Value;

        public CoinsCountChangeSignal(int value)
        {
            Value = value;
        }
    }
}