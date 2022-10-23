using System;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
   [SerializeField] private Button resumeButton;
   [SerializeField] private Button menuButton;
   
   public event Action OnResumeClick;
   public event Action OnMenuClick;
   

   protected override void Subscribe()
   {
      resumeButton.onClick.AddListener(ResumeButtonClick);
      menuButton.onClick.AddListener(MenuButtonClick);
   }
    
   protected override void Unsubscribe()
   {
      resumeButton.onClick.RemoveListener(ResumeButtonClick);
      menuButton.onClick.RemoveListener(MenuButtonClick);
   }

   private void ResumeButtonClick()
   {
      OnResumeClick?.Invoke();
   }
   
   private void MenuButtonClick()
   {
      OnMenuClick?.Invoke();
   }
   
}
