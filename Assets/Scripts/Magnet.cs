using UnityEngine;

public class Magnet : Item
{
    private void Update()
    {
        if (isCollectorDetected(out IItemsCollector itemsCollector))
        {
            itemsCollector.PickUp(this);
        }
    }
}
