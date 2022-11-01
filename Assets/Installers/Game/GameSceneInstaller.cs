using System;
using Zenject;
using Signals;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();

        BindSignals();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<StartGameSignal>();
        Container.DeclareSignal<OnCoinsAddSignal>();
        Container.DeclareSignal<OnLoseSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<PlaySignal>();
        Container.DeclareSignal<MenuSignal>();
        Container.DeclareSignal<ExitGameSignal>();
        Container.DeclareSignal<OnAppFocusChangeSignal>();
    }
}