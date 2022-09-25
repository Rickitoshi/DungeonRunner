using TMPro;
using UnityEngine;
using DG.Tweening;


public class CoinCounter : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    
    public int Coins { get; private set; }
    
    private TextMeshProUGUI _textMesh;
    
    void Awake()
    {
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(int coins)
    {
        Coins = coins;
        _textMesh.text = Coins.ToString();
    }

    public void AddCoins(int cost)
    {
        DOTween.To(value => { _textMesh.text = ((int)value).ToString(); }, Coins, Coins += cost, duration);
    }

    public void RemoveCoins(int cost)
    {
        if (Coins < 0)
        {
            DOTween.To(value => { _textMesh.text = ((int)value).ToString(); }, Coins, Coins -= cost, duration); 
        }
    }

}
