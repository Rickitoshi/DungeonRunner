using Signals;

namespace Game.UI.Buttons.Game
{
    public class ExitButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<ExitGameSignal>();
        }
    }
}