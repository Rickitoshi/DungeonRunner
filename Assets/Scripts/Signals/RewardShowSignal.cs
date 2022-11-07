namespace Signals
{
    public class RewardShowSignal
    {
        public readonly string PlacementName;

        public RewardShowSignal(string placementName)
        {
            PlacementName = placementName;
        }
    }
}