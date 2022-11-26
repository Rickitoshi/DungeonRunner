using Zenject;
using Signals;


public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AdsManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();

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
    }
}