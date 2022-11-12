using Signals;

namespace Game.UI.Buttons.Game
{
    public class MarketButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<MarketSignal>();
        }
    }
}