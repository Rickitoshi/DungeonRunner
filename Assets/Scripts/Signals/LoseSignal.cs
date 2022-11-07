namespace Signals
{
    public class LoseSignal
    {
        public readonly RoadPart RoadPart;

        public LoseSignal(RoadPart roadPart)
        {
            RoadPart = roadPart;
        }
    }
}