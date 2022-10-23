using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : BasePanel
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button x2Button;

    public event Action OnMenuClick;
    public event Action OnX2Click;
    
    protected override void Subscribe()
    {
        menuButton.onClick.AddListener(MenuButtonClick);
        x2Button.onClick.AddListener(X2ButtonClick);
    }

    protected override void Unsubscribe()
    {
        menuButton.onClick.RemoveListener(MenuButtonClick);
        x2Button.onClick.RemoveListener(X2ButtonClick);
    }
    
    private void X2ButtonClick()
    {
        OnX2Click?.Invoke();
    }
   
    private void MenuButtonClick()
    {
        OnMenuClick?.Invoke();
    }
}
