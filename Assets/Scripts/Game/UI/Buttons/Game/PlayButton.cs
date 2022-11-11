using Signals;

namespace Game.UI.Buttons.Game
{
    public class PlayButton : BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<PlaySignal>();
        }
    }
}