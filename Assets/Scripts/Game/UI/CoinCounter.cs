using TMPro;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private TextMeshProUGUI textMesh;
    
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
        DOTween.To(value => { textMesh.text = ((int)value).ToString(); }, _coins, _coins += cost, duration);
    }

    public void RemoveCoins(int cost)
    {
        DOTween.To(value => { textMesh.text = ((int)value).ToString(); }, _coins, _coins -= cost, duration);
    }

}
