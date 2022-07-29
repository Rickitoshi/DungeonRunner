using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int Cost;

    private void Update()
    {
        if (isCollectorDetected(out IItemsCollector itemsCollector))
        {
            itemsCollector.PickUp(this, Cost);
        }
    }
    
}
