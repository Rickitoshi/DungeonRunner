using System;
using Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.UI.Buttons
{
    public class AdsButton: BaseButton<RewardShowSignal>
    {
        [SerializeField] private string placementName;

        protected override void Start()
        {
            base.Start();
            _signalBus.Subscribe<MenuSignal>(Reset);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Unsubscribe<MenuSignal>(Reset);
        }

        protected override void OnClick()
        {
            _signalBus.Fire(new RewardShowSignal(placementName));
            _button.interactable = false;
        }

        private void Reset()
        {
            _button.interactable = true;
        }
    }
}