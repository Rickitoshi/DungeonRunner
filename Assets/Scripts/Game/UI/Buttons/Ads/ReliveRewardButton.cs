using Signals;

namespace Game.UI.Buttons.Ads
{
    public class ReliveRewardButton: BaseRewardButton
    {
        protected override void OnClick()
        {
            SetInteractable(false);
            
#if  UNITY_EDITOR
            
            SignalBus.Fire<ReliveSignal>();
            
#elif UNITY_ANDROID
            
            IronSource.Agent.showRewardedVideo(AdsManager.RELIVE_REWRD);
#endif
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            SignalBus.Subscribe<MenuSignal>(ResetInteractable);
            SignalBus.Subscribe<LoseSignal>(CheckAvailable);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            SignalBus.Unsubscribe<MenuSignal>(ResetInteractable);
            SignalBus.Unsubscribe<LoseSignal>(CheckAvailable);
        }
        
        
        protected override void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            if (placement.getPlacementName() == AdsManager.RELIVE_REWRD)
            {
                SignalBus.Fire<ReliveSignal>();
            }
        }
    }
}