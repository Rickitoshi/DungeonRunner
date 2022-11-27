using System;
using DG.Tweening;
using Game.Systems;
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
        _signalBus.Subscribe<PlayerDieSignal>(Lose);
        _signalBus.Subscribe<CoinsPickUpSignal>(AddCoins);
        _signalBus.Subscribe<BackToLobbySignal>(Restart);
        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<PlaySignal>(Play);
        _signalBus.Subscribe<ExitGameSignal>(Exit);
        _signalBus.Subscribe<AppFocusChangeSignal>(SaveData);
        _signalBus.Subscribe<ReliveSignal>(Relive);
        _signalBus.Subscribe<PlayerRespawnPhaseEndedSignal>(PlayerRespawnPhaseEnded);
    }
    
    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<PlayerDieSignal>(Lose);
        _signalBus.Unsubscribe<CoinsPickUpSignal>(AddCoins);
        _signalBus.Unsubscribe<BackToLobbySignal>(Restart);
        _signalBus.Unsubscribe<PauseSignal>(Pause);
        _signalBus.Unsubscribe<PlaySignal>(Play);
        _signalBus.Unsubscribe<ExitGameSignal>(Exit);
        _signalBus.Unsubscribe<AppFocusChangeSignal>(SaveData);
        _signalBus.Unsubscribe<ReliveSignal>(Relive);
        _signalBus.Unsubscribe<PlayerRespawnPhaseEndedSignal>(PlayerRespawnPhaseEnded);
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void Play()
    {
        Time.timeScale = 1;
        
        _player.State = State.Run;

        var properties = new Value
        {
            ["Start coins value"] = _coins
        };
        Mixpanel.Track("Start run", properties);
    }

    private void Restart()
    {
        Time.timeScale = 1;
        
        if (_player.State == State.Run)
        {
            _player.State = State.Idle;
        }
        
        _player.Respawn();
    }

    private void Relive()
    {
        _player.State = State.Run;
        _player.Relive();
        Mixpanel.Track("Relive reward");
    }

    private void PlayerRespawnPhaseEnded(PlayerRespawnPhaseEndedSignal signal)
    {
        if (signal.PhaseEnded == RespawnPhaseEnded.Begin)
        {
            _player.State = State.Idle;
            _roadManager.Restart();
        }
    }
    
    private void Lose()
    {
        _player.State = State.Die;

        Mixpanel.Track("Player die");
    }

    private void AddCoins(CoinsPickUpSignal signal)
    {
        if (signal.Value < 0) return;
        
        _coins += signal.Value;
        _saveManager.Data.Coins = _coins;
    }
}
