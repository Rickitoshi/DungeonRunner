using Signals;

namespace Game.UI.Buttons.Ads
{
    public class CoinsRewardButton: BaseRewardButton
    {
        protected override void OnClick()
        {
#if  UNITY_EDITOR

            SignalBus.Fire(new CoinsAddSignal(200));

#elif UNITY_ANDROID

            IronSource.Agent.showRewardedVideo(AdsManager.COINS_REWARD);
#endif
        }
        
        protected override void Subscribe()
        {
            base.Subscribe();
            
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            
        }
        
        protected override void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            if (placement.getPlacementName() == AdsManager.COINS_REWARD)
            {
                SignalBus.Fire(new CoinsAddSignal(placement.getRewardAmount()));
            }
        }
    }
}