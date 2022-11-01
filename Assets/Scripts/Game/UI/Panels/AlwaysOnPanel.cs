using UnityEngine;

public class AlwaysOnPanel : BasePanel
{
    [SerializeField] private CoinCounter coinCounter;

    public CoinCounter CoinCounter => coinCounter;
}