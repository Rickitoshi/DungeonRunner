using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Buttons.Game
{
    public abstract class BaseButton: MonoBehaviour
    {
        [Inject] protected SignalBus SignalBus;
        protected Button Button;

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected virtual void Start()
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