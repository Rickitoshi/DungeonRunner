using UnityEngine;
using DG.Tweening;
using Game.UI.Common;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private float _alphaDuration;

    public virtual void Initialize(PanelAnimationConfig config)
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _alphaDuration = config.PanelDurationAlpha;
        
        gameObject.SetActive(false);
        _canvasGroup.alpha = 0;
    }
    
    public virtual void Activate()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, _alphaDuration).SetUpdate(true);
    }
    
    public virtual void Deactivate()
    {
        _canvasGroup.DOFade(0, _alphaDuration).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetInteractable(bool value)
    {
        _canvasGroup.interactable = value;
    }
}
