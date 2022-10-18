using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [SerializeField] private CoinCounter coinCounter;
    [SerializeField] private Button pauseButton;

    public CoinCounter CoinCounter => coinCounter;

    public event Action OnPauseClick;
    
    
    protected override void Subscribe()
    {
        pauseButton.onClick.AddListener(PauseButtonClick);
    }

    protected override void Unsubscribe()
    {
        pauseButton.onClick.RemoveListener(PauseButtonClick);
    }

    private void PauseButtonClick()
    {
        OnPauseClick?.Invoke();
    }
}
