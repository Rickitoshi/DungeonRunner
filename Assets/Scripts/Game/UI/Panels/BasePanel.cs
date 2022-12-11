using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize()
    {
        gameObject.SetActive(false);
    }
    
    public void Activate()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.1f).SetUpdate(true);
    }
    
    public void Deactivate()
    {
        _canvasGroup.DOFade(0, 0.1f).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetInteractable(bool value)
    {
        _canvasGroup.interactable = value;
    }
}
