using System;
using Signals;
using Zenject;

namespace Game.Player.Stats
{
    public class StatsContainer: IInitializable,IDisposable
    {
        [Inject] private SaveManager _saveManager;
        [Inject] private StatsConfig _statsConfig;
        [Inject] private SignalBus _signalBus;

        public int Agility => _agility.Levels[_levelStatsIndex[0]].Value;
        public int Damage => _damage.Levels[_levelStatsIndex[1]].Value;
        public int Defence => _defence.Levels[_levelStatsIndex[2]].Value;
        public int Health => _health.Levels[_levelStatsIndex[3]].Value;
        public int Magnet => _magnet.Levels[_levelStatsIndex[4]].Value;

        private int[] _levelStatsIndex;

        private Stat _agility;
        private Stat _damage;
        private Stat _defence;
        private Stat _health;
        private Stat _magnet;
        
        public void Initialize()
        {
            _levelStatsIndex = _saveManager.Data.LevelStatsIndex;

            _agility = _statsConfig.GetStatByID(1);
            _damage = _statsConfig.GetStatByID(2);
            _defence = _statsConfig.GetStatByID(3);
            _health = _statsConfig.GetStatByID(4);
            _magnet = _statsConfig.GetStatByID(5);

            Subscribe();
        }
        
        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<OnUpgradeSignal>(OnUpgrade);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<OnUpgradeSignal>(OnUpgrade);
        }
        
        private void OnUpgrade(OnUpgradeSignal signal)
        {
            _levelStatsIndex[signal.StatID - 1]++;
            _saveManager.Data.LevelStatsIndex = _levelStatsIndex;
        }
    }
}