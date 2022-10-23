using System;
using Signals;
using Zenject;

public class UIManager: IInitializable, IDisposable
{
    [Inject] private GamePanel _gamePanel;
    [Inject] private PausePanel _pausePanel;
    [Inject] private LosePanel _losePanel;
    [Inject] private MenuPanel _menuPanel;
    [Inject] private SignalBus _signalBus;
    [Inject] private SaveSystem _saveSystem;

    private BasePanel _currentPanel;

    public void Initialize()
    {
        _gamePanel.CoinCounter.Initialize(_saveSystem.Data.Coins);
        
        _gamePanel.Deactivate();
        _pausePanel.Deactivate();
        _losePanel.Deactivate();
        _menuPanel.Deactivate();

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
        _signalBus.Subscribe<ResumeSignal>(OnGame);
        _signalBus.Subscribe<OnLoseSignal>(OnLose);
        _signalBus.Subscribe<StartGameSignal>(OnGame);
        _signalBus.Subscribe<OnCoinsAddSignal>(AddCoins);
    }

    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<MenuSignal>(OnMenu);
        _signalBus.Unsubscribe<PauseSignal>(OnPause);
        _signalBus.Unsubscribe<ResumeSignal>(OnGame);
        _signalBus.Unsubscribe<OnLoseSignal>(OnLose);
        _signalBus.Unsubscribe<StartGameSignal>(OnGame);
        _signalBus.Unsubscribe<OnCoinsAddSignal>(AddCoins);
    }

    private void OnMenu()
    {
        ChangePanel(_menuPanel);
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

    private void AddCoins(OnCoinsAddSignal signal)
    {
        _gamePanel.CoinCounter.AddCoins(signal.Value);
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
