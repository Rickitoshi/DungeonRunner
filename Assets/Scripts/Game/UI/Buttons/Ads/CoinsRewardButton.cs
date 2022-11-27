using Signals;
using TMPro;
using UnityEngine;

namespace Game.UI.Buttons.Ads
{
    public class CoinsRewardButton: BaseRewardButton
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        
        private int _coinsReward;
        
        protected override void OnClick()
        {
            IronSource.Agent.showRewardedVideo(AdsManager.COINS_REWARD);
        }
        
        [ContextMenu("GetReward")]
        protected override void GetReward()
        {
            SignalBus.Fire(new CoinsPickUpSignal(_coinsReward));
        }
        
        protected override void OnAvailableChange(bool value)
        {
            if (!value)
            {
                SetInteractable(false);
                return;
            }
            
            _coinsReward = IronSource.Agent.getPlacementInfo(AdsManager.COINS_REWARD).getRewardAmount();
            textMesh.text = $"+{_coinsReward}";

            SetInteractable(!IronSource.Agent.isRewardedVideoPlacementCapped(AdsManager.COINS_REWARD));
        }
        
        protected override void OnRewardVideoFinish(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            if (placement.getPlacementName() == AdsManager.COINS_REWARD)
            {
                GetReward();
            }
        }
    }
}