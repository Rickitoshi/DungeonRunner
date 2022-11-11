using Game.UI.Buttons.Game;

namespace Game.UI.Buttons.Ads
{
    public abstract class BaseRewardButton: BaseButton
    {
        protected override void Subscribe()
        {
            base.Subscribe();
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardVideoFinish;
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardVideoFinish;
        }
        
        protected abstract void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info);

        protected void CheckAvailable()
        {
            if (!IronSource.Agent.isRewardedVideoAvailable()) SetInteractable(false);
        }

        protected void ResetInteractable()
        {
            SetInteractable(true);
        }

        protected void SetInteractable(bool value)
        {
            Button.interactable = value;
        }
    }
}