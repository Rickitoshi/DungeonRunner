using Signals;
using UnityEngine;

namespace Game.UI.Buttons.Ads
{
    public class ReliveRewardButton: BaseRewardButton
    {
        private bool _isRewarded;
        
        protected override void OnClick()
        {
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
            SignalBus.Subscribe<BackToLobbySignal>(Reset);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            SignalBus.Unsubscribe<BackToLobbySignal>(Reset);
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
                _isRewarded = true;
                SetInteractable(false);
            }
        }
    }
}