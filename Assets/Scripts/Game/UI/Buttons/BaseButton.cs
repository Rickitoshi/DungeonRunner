using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Buttons
{
    public abstract class BaseButton<T>: MonoBehaviour
    {
        private SignalBus _signalBus;
        private Button _button;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _signalBus.Fire<T>();
        }
    }
}