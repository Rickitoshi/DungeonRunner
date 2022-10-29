using TMPro;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float coinViewScale;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private RectTransform coinView;
    
    private int _coins;
    private SignalBus _signalBus;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(int coins)
    {
        _coins = coins;
        textMesh.text = _coins.ToString();
    }

    public void AddCoins(int cost)
    {
        if (isActiveAndEnabled)
        {
            DOTween.Rewind(coinView);
            coinView.DOScale(coinViewScale, duration * 0.5f).SetLoops(2, LoopType.Yoyo);
            DOTween.To(value => { textMesh.text = ((int)value).ToString(); }, _coins, _coins += cost, duration); 
        }
        else
        {
            _coins += cost;
            textMesh.text = _coins.ToString();
        }
    }

    public void RemoveCoins(int cost)
    {
        DOTween.Rewind(coinView);
        coinView.DOScale(coinViewScale, duration * 0.5f).SetLoops(2, LoopType.Yoyo);
        DOTween.To(value => { textMesh.text = ((int)value).ToString(); }, _coins, _coins -= cost, duration);
    }

}
