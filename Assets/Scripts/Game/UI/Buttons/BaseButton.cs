using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Buttons
{
    public abstract class BaseButton<T>: MonoBehaviour
    {
        protected SignalBus _signalBus;
        protected Button _button;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        protected virtual void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            _signalBus.Fire<T>();
        }
    }
}