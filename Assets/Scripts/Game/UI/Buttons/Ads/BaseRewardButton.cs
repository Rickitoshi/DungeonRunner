using Game.UI.Buttons.Game;
using Zenject;

namespace Game.UI.Buttons.Ads
{
    public abstract class BaseRewardButton: BaseButton
    {
        protected override void Start()
        {
            base.Start();
            SetInteractable(false);
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardVideoFinish;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += OnAvailableChange;
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardVideoFinish;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= OnAvailableChange;
        }
        
        protected void SetInteractable(bool value)
        {
            Button.interactable = value;
        }
        
        protected abstract void GetReward();
        
        protected abstract void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info);

        protected abstract void OnAvailableChange(bool value);

    }
}