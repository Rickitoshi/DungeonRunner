using Zenject;
using Signals;


public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AdsManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();

        BindSignals();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<CoinsAddSignal>();
        Container.DeclareSignal<LoseSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<PlaySignal>();
        Container.DeclareSignal<MenuSignal>();
        Container.DeclareSignal<ExitGameSignal>();
        Container.DeclareSignal<AppFocusChangeSignal>();
        Container.DeclareSignal<ReliveSignal>();
        Container.DeclareSignal<MarketSignal>();
    }
}