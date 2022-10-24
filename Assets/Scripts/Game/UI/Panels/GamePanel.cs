using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [SerializeField] private CoinCounter coinCounter;

    public CoinCounter CoinCounter => coinCounter;
}
