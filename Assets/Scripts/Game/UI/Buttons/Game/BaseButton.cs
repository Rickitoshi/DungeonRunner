using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Buttons.Game
{
    public abstract class BaseButton: MonoBehaviour
    {
        protected SignalBus SignalBus;
        protected Button Button;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        private void Start()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        protected abstract void OnClick();

        protected virtual void Subscribe()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected virtual void Unsubscribe()
        {
            Button.onClick.RemoveListener(OnClick);
        }
    }
}