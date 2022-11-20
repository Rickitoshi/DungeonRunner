using Signals;
using TMPro;
using UnityEngine;

namespace Game.UI.Buttons.Ads
{
    public class CoinsRewardButton: BaseRewardButton
    {
        protected override void OnClick()
        {
#if  UNITY_EDITOR

            SignalBus.Fire(new CoinsAddSignal(500));

//#elif UNITY_ANDROID

            IronSource.Agent.showRewardedVideo(AdsManager.COINS_REWARD);
#endif
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            SignalBus.Subscribe<MarketSignal>(CheckAvailable);
            
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            SignalBus.Unsubscribe<MarketSignal>(CheckAvailable);
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