using DG.Tweening;
using Game.UI.Common;
using UnityEngine;

namespace Game.UI.Panels
{
    public class PopUp: MonoBehaviour
    {
        private RectTransform _rectTransform;
        private float _scale;
        private float _scaleDuration;

        public void Initialize(PanelAnimationConfig config)
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _scale = config.PopUpStartScale;
            _scaleDuration = config.PopUpDurationScale;

            _rectTransform.localScale = new Vector3(_scale,_scale,_scale);
        }
        
        public void Show()
        {
            _rectTransform.DOScale(1, _scaleDuration).SetEase(Ease.OutBack).SetUpdate(true);
        }

        public void Hide()
        {
             _rectTransform.DOScale(_scale, _scaleDuration).SetUpdate(true);
        }
    }
}