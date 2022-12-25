using System;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Common
{
    public class UpgradeUI : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Stat, int, UpgradeUI>
        {
         
        }
        
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI currentValue;
        [SerializeField] private TextMeshProUGUI nextValue;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Button button;

        [Inject] private SignalBus _signalBus;
        [Inject] private Stat _stat;
        [Inject] private int _levelIndex;
        
        private bool _isMaxLevel;

        public void Initialize()
        {
            Subscribe();
            
            statName.text = _stat.name;
            currentValue.text = _stat.Levels[_levelIndex].Value.ToString();

            CheckMaxLevel();
        }

        
        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            button.onClick.AddListener(OnClick);
            _signalBus.Subscribe<CoinsCountChangeSignal>(CheckUpdateAvailable);
        }

        private void Unsubscribe()
        {
            button.onClick.RemoveListener(OnClick);
            _signalBus.Unsubscribe<CoinsCountChangeSignal>(CheckUpdateAvailable);
        }
        
        private void OnClick()
        {
            Upgrade();
        }
        
        private void CheckUpdateAvailable(CoinsCountChangeSignal signal)
        {
            if(_isMaxLevel) return;
            
            button.interactable = signal.Value >= _stat.Levels[_levelIndex + 1].Price;
        }
        
        private void CheckMaxLevel()
        {
            if (_levelIndex >= _stat.Levels.Length - 1)
            {
                button.interactable = false;
                nextValue.gameObject.SetActive(false);
                buttonText.text = "Max";
                _isMaxLevel = true;
            }
            else
            {
                button.interactable = true;
                buttonText.text = _stat.Levels[_levelIndex + 1].Price.ToString();
                nextValue.text = _stat.Levels[_levelIndex + 1].Value.ToString();
            }
        }
        
        private void Upgrade()
        {
            _signalBus.Fire(new OnUpgradeSignal(_stat.ID));
            
            _levelIndex++;
            CheckMaxLevel();
            
            _signalBus.Fire(new CoinsSpendSignal(_stat.Levels[_levelIndex].Price));
            currentValue.text = _stat.Levels[_levelIndex].Value.ToString();
            
        }
    }
}