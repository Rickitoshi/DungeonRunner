using Signals;

namespace Game.UI.Buttons.Game
{
    public class PauseButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<PauseSignal>();
        }
    }
}