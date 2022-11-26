using Signals;

namespace Game.UI.Buttons.Game
{
    public class BackToMenuButton : BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<BackToMenuSignal>();
        }
    }
}