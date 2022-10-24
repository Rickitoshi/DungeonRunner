using System;
using Signals;
using UnityEngine;
using Zenject;

public class GameManager: IInitializable, IDisposable
{
    [Inject] private RoadManager _roadManager;
    [Inject] private CameraManager _cameraManager;
    [Inject] private SignalBus _signalBus;
    [Inject] private SaveSystem _saveSystem;
    [Inject] private PlayerController _player;

    private int _coins;

    public void Initialize()
    {
        _coins = _saveSystem.Data.Coins;
        Subscribe();
    }
    
    public void Dispose()
    {
        Exit();
    }

    private void Exit()
    {
        _saveSystem.SaveData();
        Unsubscribe();
        Application.Quit();
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<OnLoseSignal>(Lose);
        _signalBus.Subscribe<OnCoinsAddSignal>(AddCoins);
        _signalBus.Subscribe<MenuSignal>(Restart);
        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<ResumeSignal>(Resume);
        _signalBus.Subscribe<ExitGameSignal>(Exit);
    }
    
    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<OnLoseSignal>(Lose);
        _signalBus.Unsubscribe<OnCoinsAddSignal>(AddCoins);
        _signalBus.Unsubscribe<MenuSignal>(Restart);
        _signalBus.Unsubscribe<PauseSignal>(Pause);
        _signalBus.Unsubscribe<ResumeSignal>(Resume);
        _signalBus.Unsubscribe<ExitGameSignal>(Exit);
    }

    private void Pause()
    {
        _player.State = State.Idle;
    }

    private void Resume()
    {
        _cameraManager.SetLobbyCamera(false);
        _player.State = State.Run;
    }

    private void Restart()
    {
        _cameraManager.SetLobbyCamera(true);
        _player.SetDefault();
        _roadManager.Restart();
    }
    
    private void Lose()
    {
        _player.State = State.Die;
    }

    private void AddCoins(OnCoinsAddSignal signal)
    {
        _coins += signal.Value;
        _saveSystem.Data.Coins = _coins;
    }
}