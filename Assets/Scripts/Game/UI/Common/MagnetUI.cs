using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Common
{
    public class MagnetUI : MonoBehaviour
    {
        [SerializeField] private Image image;

        private void Start()
        {
            image.fillAmount = 0;
        }

        public void Start(float duration)
        {
            DOTween.Rewind(gameObject);
            image.fillAmount = 1;
            DOTween.To(v => { image.fillAmount = v; }, image.fillAmount, 0, duration);
        }

        public void Stop()
        {
            DOTween.Rewind(gameObject);
            image.fillAmount = 0;
        }
    }
}