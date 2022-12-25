namespace Signals
{
    public class OnUpgradeSignal
    {
        public readonly int StatID;

       public OnUpgradeSignal(int id)
        {
            StatID = id;
        }
    }
}