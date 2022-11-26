using Signals;

namespace Game.UI.Buttons.Game
{
    public class BackToLobbyButton: BaseButton
    {
        protected override void OnClick()
        {
            SignalBus.Fire<BackToLobbySignal>();
        }
    }
}