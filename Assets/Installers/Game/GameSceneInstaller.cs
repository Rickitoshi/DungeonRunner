using Game.Player.Stats;
using Game.UI.Common;
using Zenject;
using Signals;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private UpgradeUI upgradeUIPrefab;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AdsManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StatsContainer>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        Container.BindFactory<Stat, int, UpgradeUI, UpgradeUI.Factory>().FromComponentInNewPrefab(upgradeUIPrefab);

        BindSignals();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<CoinsPickUpSignal>();
        Container.DeclareSignal<PlayerDieSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<PlaySignal>();
        Container.DeclareSignal<BackToLobbySignal>();
        Container.DeclareSignal<ExitGameSignal>();
        Container.DeclareSignal<AppFocusChangeSignal>();
        Container.DeclareSignal<ReliveSignal>();
        Container.DeclareSignal<MarketSignal>();
        Container.DeclareSignal<MagnetSignal>();
        Container.DeclareSignal<PlayerRespawnPhaseEndedSignal>();
        Container.DeclareSignal<BackToMenuSignal>();
        Container.DeclareSignal<UpgradeSignal>();
        Container.DeclareSignal<CoinsCountChangeSignal>();
        Container.DeclareSignal<CoinsSpendSignal>();
        Container.DeclareSignal<OnUpgradeSignal>();
    }
}