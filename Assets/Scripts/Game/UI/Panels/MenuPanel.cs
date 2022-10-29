using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : BasePanel
{
    [SerializeField] private CoinCounter coinCounter;

    public CoinCounter CoinCounter => coinCounter;
}

