using System;
using Game.Player;
using Game.Systems;
using Signals;
using Zenject;

public class UIManager: IInitializable, IDisposable
{
    [Inject] private GamePanel _gamePanel;
    [Inject] private PausePanel _pausePanel;
    [Inject] private LosePanel _losePanel;
    [Inject] private MenuPanel _menuPanel;
    [Inject] private MarketPanel _marketPanel;
    [Inject] private AlwaysOnPanel _alwaysOnPanel;
    
    [Inject] private SignalBus _signalBus;
    [Inject] private SaveManager _saveManager;
    [Inject] private PlayerConfig _playerConfig;
    
    private BasePanel _currentPanel;

    public void Initialize()
    {
        _alwaysOnPanel.CoinCounter.Initialize(_saveManager.Data.Coins);

        _gamePanel.Initialize();
        _pausePanel.Initialize();
        _losePanel.Initialize();
        _menuPanel.Initialize();
        _marketPanel.Initialize();

        ChangePanel(_menuPanel);
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<BackToMenuSignal>(OnMenu);
        _signalBus.Subscribe<BackToLobbySignal>(OnLobby);
        _signalBus.Subscribe<PauseSignal>(OnPause);
        _signalBus.Subscribe<PlaySignal>(OnGame);
        _signalBus.Subscribe<PlayerDieSignal>(OnLose);
        _signalBus.Subscribe<CoinsPickUpSignal>(AddCoins);
        _signalBus.Subscribe<ReliveSignal>(OnGame);
        _signalBus.Subscribe<MarketSignal>(OnMarket);
        _signalBus.Subscribe<MagnetSignal>(OnMagnet);
        _signalBus.Subscribe<PlayerRespawnPhaseEndedSignal>(PlayerRespawnPhaseEnded);
    }

    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<BackToMenuSignal>(OnMenu);
        _signalBus.Unsubscribe<BackToLobbySignal>(OnLobby);
        _signalBus.Unsubscribe<PauseSignal>(OnPause);
        _signalBus.Unsubscribe<PlaySignal>(OnGame);
        _signalBus.Unsubscribe<PlayerDieSignal>(OnLose);
        _signalBus.Unsubscribe<CoinsPickUpSignal>(AddCoins);
        _signalBus.Unsubscribe<ReliveSignal>(OnGame);
        _signalBus.Unsubscribe<MarketSignal>(OnMarket);
        _signalBus.Unsubscribe<MagnetSignal>(OnMagnet);
        _signalBus.Unsubscribe<PlayerRespawnPhaseEndedSignal>(PlayerRespawnPhaseEnded);
    }
    
    private void OnMenu()
    {
        ChangePanel(_menuPanel);
        GameHelper.Instance.CameraState = CameraState.Lobby;
    }

    private void OnLobby()
    {
        _gamePanel.Magnet.Stop();
        _currentPanel.Deactivate();
    }

    private void OnMarket()
    {
        ChangePanel(_marketPanel);
        GameHelper.Instance.CameraState = CameraState.Market;
    }

    private void OnGame()
    {
        ChangePanel(_gamePanel);
        GameHelper.Instance.CameraState = CameraState.Run;
    }

    private void OnPause()
    {
        ChangePanel(_pausePanel);
    }

    private void OnLose()
    {
        ChangePanel(_losePanel);
    }

    private void PlayerRespawnPhaseEnded(PlayerRespawnPhaseEndedSignal signal)
    {
        switch (signal.PhaseEnded)
        {
            case RespawnPhaseEnded.Begin:
                OnMenu();
                _currentPanel.SetInteractable(false);
                break;
            case RespawnPhaseEnded.Finish:
                _currentPanel.SetInteractable(true);
                break;
        }
    }

    private void OnMagnet()
    {
        _gamePanel.Magnet.Start(_playerConfig.MagnetDuration);
    }

    private void AddCoins(CoinsPickUpSignal signal)
    {
        _alwaysOnPanel.CoinCounter.AddCoins(signal.Value);
    }

    private void ChangePanel(BasePanel panel)
    {
        if (_currentPanel != null)
        {
            _currentPanel.Deactivate();
        }

        _currentPanel = panel;
        _currentPanel.Activate();
    }
}
