using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int Coins => _counter.Coins;
    private CoinCounter _counter;

    private void Awake()
    {
        _counter = GetComponentInChildren<CoinCounter>();
    }

    public void Initialize(int coins)
    {
        _counter.Initialize(coins);
    }

    public void AddCoins(int cost)
    {
        _counter.AddCoins(cost);
    }
    
}
