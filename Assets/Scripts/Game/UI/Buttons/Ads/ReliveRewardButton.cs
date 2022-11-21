using Signals;
using UnityEngine;

namespace Game.UI.Buttons.Ads
{
    public class ReliveRewardButton: BaseRewardButton
    {
        private bool _isRewarded;
        
        protected override void OnClick()
        {
            _isRewarded = true;
            SetInteractable(false);
            IronSource.Agent.showRewardedVideo(AdsManager.RELIVE_REWRD);
        }

        [ContextMenu("GetReward")]
        protected override void GetReward()
        {
            SignalBus.Fire<ReliveSignal>();
        }
        
        protected override void Subscribe()
        {
            base.Subscribe();
            SignalBus.Subscribe<MenuSignal>(Reset);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            SignalBus.Unsubscribe<MenuSignal>(Reset);
        }

        private void Reset()
        {
            _isRewarded = false;
        }

        protected override void OnAvailableChange(bool value)
        {
            if (!_isRewarded)
            {
                SetInteractable(value);
            }
        }
        
        protected override void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            if (placement.getPlacementName() == AdsManager.RELIVE_REWRD)
            {
                GetReward();
            }
        }
    }
}