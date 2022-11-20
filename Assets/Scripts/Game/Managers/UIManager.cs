using System;
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
        _signalBus.Subscribe<MenuSignal>(OnMenu);
        _signalBus.Subscribe<PauseSignal>(OnPause);
        _signalBus.Subscribe<PlaySignal>(OnGame);
        _signalBus.Subscribe<LoseSignal>(OnLose);
        _signalBus.Subscribe<CoinsAddSignal>(AddCoins);
        _signalBus.Subscribe<ReliveSignal>(OnGame);
        _signalBus.Subscribe<MarketSignal>(OnMarket);
        _signalBus.Subscribe<MagnetSignal>(OnMagnet);
    }

    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<MenuSignal>(OnMenu);
        _signalBus.Unsubscribe<PauseSignal>(OnPause);
        _signalBus.Unsubscribe<PlaySignal>(OnGame);
        _signalBus.Unsubscribe<LoseSignal>(OnLose);
        _signalBus.Unsubscribe<CoinsAddSignal>(AddCoins);
        _signalBus.Unsubscribe<ReliveSignal>(OnGame);
        _signalBus.Unsubscribe<MarketSignal>(OnMarket);
        _signalBus.Unsubscribe<MagnetSignal>(OnMagnet);
    }

    private void OnMenu()
    {
        ChangePanel(_menuPanel);
        GameHelper.Instance.CameraState = CameraState.Lobby;
    }

    private void OnMarket()
    {
        ChangePanel(_marketPanel);
        GameHelper.Instance.CameraState = CameraState.Market;
    }

    private void OnGame()
    {
        ChangePanel(_gamePanel);
    }

    private void OnPause()
    {
        ChangePanel(_pausePanel);
    }

    private void OnLose()
    {
        ChangePanel(_losePanel);
    }

    private void OnMagnet(MagnetSignal signal)
    {
        _gamePanel.Magnet.Execute(signal.Duration);
    }

    private void AddCoins(CoinsAddSignal signal)
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
