using Game.UI.Common;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
    public class UpgradesPanel : BasePopUpPanel
    {
        [SerializeField] private Transform upgradesContainer;

        [Inject] private StatsConfig _config;
        [Inject] private UpgradeUI.Factory _factory;
        [Inject] private SaveManager _saveManager;

        public override void Initialize(PanelAnimationConfig config)
        {
            base.Initialize(config);

            for (int i = 0; i < _config.Stats.Length; i++)
            {
                var element = _factory.Create(_config.GetStatByID(i + 1), _saveManager.Data.LevelStatsIndex[i]);
                element.transform.SetParent(upgradesContainer,false);
                element.Initialize();
            }
        }
    }
}