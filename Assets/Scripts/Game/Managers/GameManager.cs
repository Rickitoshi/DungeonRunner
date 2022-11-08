using System;
using Signals;
using UnityEngine;
using Zenject;
using mixpanel;

public class GameManager: IInitializable,IDisposable
{
    [Inject] private RoadManager _roadManager;
    [Inject] private SignalBus _signalBus;
    [Inject] private SaveSystem _saveSystem;
    [Inject] private PlayerController _player;

    private int _coins;
    private RoadPart _dieRoadPart;

    public void Initialize()
    {
        Mixpanel.Track("Startup game");
        
        _coins = _saveSystem.Data.Coins;
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    private void SaveData()
    {
        _saveSystem.SaveData();
    }
    
    private void Exit()
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
        Helper.Instance.LobbyCamera.SetActive(false);
        _player.State = State.Run;

        var properties = new Value
        {
            ["Start coins value"] = _coins
        };
        Mixpanel.Track("Start run", properties);
    }

    private void Restart()
    {
        Helper.Instance.LobbyCamera.SetActive(true);
        _player.SetLobby();
        _roadManager.Restart();
    }

    private void Relive()
    {
        _player.State = State.Run;
        _dieRoadPart.DeactivateItemsAndObstacles();
    }
    
    private void Lose(LoseSignal signal)
    {
        _player.State = State.Die;
        _dieRoadPart = signal.RoadPart;
    }

    private void AddCoins(CoinsAddSignal signal)
    {
        if (signal.Value < 0) return;
        
        _coins += signal.Value;
        _saveSystem.Data.Coins = _coins;
    }
}
