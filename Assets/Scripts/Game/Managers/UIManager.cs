using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GamePanel gamePanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private LosePanel losePanel;
    [SerializeField] private BasePanel menuPanel;

    public event Action OnPause;
    public event Action OnResume;
    public event Action OnMenu;
    
    public GameScreen GameScreen
    {
        get => _currentScreen;
        set
        {
            if (value == _currentScreen) 
                return;

            switch (value)
            {
                case GameScreen.Game:
                    ChangePanel(gamePanel);
                    break;
                case GameScreen.Lose:
                    ChangePanel(losePanel);
                    break;
                case GameScreen.Menu:
                    ChangePanel(menuPanel);
                    break;
                case GameScreen.Pause:
                    ChangePanel(pausePanel);
                    break;
                
            }
        }
    }
    
    private GameScreen _currentScreen;
    private BasePanel _currentPanel;

    private void Awake()
    {
        _currentPanel = menuPanel;
    }

    private void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        pausePanel.OnMenuClick += OnMenuClick;
        pausePanel.OnResumeClick += OnResumeClick;
        gamePanel.OnPauseClick += OnPauseClick;
        losePanel.OnMenuClick += OnMenuClick;
    }
    
    private void Unsubscribe()
    {
        pausePanel.OnMenuClick -= OnMenuClick;
        pausePanel.OnResumeClick -= OnResumeClick;
        gamePanel.OnPauseClick -= OnPauseClick;
        losePanel.OnMenuClick -= OnMenuClick;
    }

    private void OnMenuClick()
    {
        GameScreen = GameScreen.Menu;
        OnMenu?.Invoke();
    }

    private void OnResumeClick()
    {
        GameScreen = GameScreen.Game;
        OnResume?.Invoke();
    }

    private void OnPauseClick()
    {
        GameScreen = GameScreen.Pause;
        OnPause?.Invoke();
    }
    
    private void ChangePanel(BasePanel panel)
    {
        _currentPanel.Deactivate();
        _currentPanel = panel;
        _currentPanel.Activate();
    }

    public void Initialize()
    {
        gamePanel.Deactivate();
        pausePanel.Deactivate();
        losePanel.Deactivate();
        menuPanel.Deactivate();
    }

    public void AddCoins(int value)
    {
        gamePanel.CoinCounter.AddCoins(value);
    }
    
}

public enum GameScreen
{
    None,
    Game,
    Lose,
    Menu,
    Pause
}
