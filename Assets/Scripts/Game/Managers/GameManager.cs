using System;
using Signals;
using UnityEngine;
using Zenject;
using mixpanel;

public class GameManager: IInitializable,IDisposable
{
    [Inject] private RoadManager _roadManager;
    [Inject] private SignalBus _signalBus;
    [Inject] private SaveManager _saveManager;
    [Inject] private PlayerController _player;

    private int _coins;

    public void Initialize()
    {
        Mixpanel.Track("Startup game");

        _player.State = State.Idle;
        _coins = _saveManager.Data.Coins;
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    private void SaveData()
    {
        _saveManager.SaveData();
    }
    
    private  void Exit()
    {
        Application.Quit();
        SaveData();
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<LoseSignal>(Lose);
        _signalBus.Subscribe<CoinsAddSignal>(AddCoins);
        _signalBus.Subscribe<MenuSignal>(Restart);
        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<PlaySignal>(Play);
        _signalBus.Subscribe<ExitGameSignal>(Exit);
        _signalBus.Subscribe<AppFocusChangeSignal>(SaveData);
        _signalBus.Subscribe<ReliveSignal>(Relive);
    }
    
    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<LoseSignal>(Lose);
        _signalBus.Unsubscribe<CoinsAddSignal>(AddCoins);
        _signalBus.Unsubscribe<MenuSignal>(Restart);
        _signalBus.Unsubscribe<PauseSignal>(Pause);
        _signalBus.Unsubscribe<PlaySignal>(Play);
        _signalBus.Unsubscribe<ExitGameSignal>(Exit);
        _signalBus.Unsubscribe<AppFocusChangeSignal>(SaveData);
        _signalBus.Unsubscribe<ReliveSignal>(Relive);
    }

    private void Pause()
    {
        _player.State = State.Idle;
    }

    private void Play()
    {
        GameHelper.Instance.CameraState = CameraState.Run;
        _player.State = State.Run;

        var properties = new Value
        {
            ["Start coins value"] = _coins
        };
        Mixpanel.Track("Start run", properties);
    }

    private void Restart()
    {
        GameHelper.Instance.CameraState = CameraState.Lobby;
        _player.SetDefault();
        _player.State = State.Idle;
        _roadManager.Restart();
    }

    private void Relive()
    {
        _player.State = State.Run;
        _player.Relive();
        Mixpanel.Track("Relive reward");
    }
    
    private void Lose()
    {
        _player.State = State.Die;

        Mixpanel.Track("Player die");
    }

    private void AddCoins(CoinsAddSignal signal)
    {
        if (signal.Value < 0) return;
        
        _coins += signal.Value;
        _saveManager.Data.Coins = _coins;
    }
}
