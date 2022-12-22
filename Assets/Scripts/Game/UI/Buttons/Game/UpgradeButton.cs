using Signals;

namespace Game.UI.Buttons.Game
{
    public class UpgradeButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<UpgradeSignal>();
        }
    }
}