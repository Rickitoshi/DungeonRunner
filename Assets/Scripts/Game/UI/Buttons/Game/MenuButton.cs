using Signals;

namespace Game.UI.Buttons.Game
{
    public class MenuButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<MenuSignal>();
        }
    }
}