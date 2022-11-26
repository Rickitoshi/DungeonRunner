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
        DOTween.To(value => { _canvasGroup.alpha = value; }, _canvasGroup.alpha, 1, 0.1f);
    }
    
    public void Deactivate()
    {
        DOTween.To(value => { _canvasGroup.alpha = value; }, _canvasGroup.alpha, 0, 0.1f).OnComplete(() => {gameObject.SetActive(false);});
    }

    public void SetInteractable(bool value)
    {
        _canvasGroup.interactable = value;
    }
}
