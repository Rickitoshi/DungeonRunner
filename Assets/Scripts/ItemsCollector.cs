using UnityEngine;

public class ItemsCollector : MonoBehaviour, IItemsCollector
{
    public void PickUp(Coin coin, int cost)
    {
        print(cost);
        Destroy(coin.gameObject);
    }

    public void PickUp(Magnet magnet)
    {
        Destroy(magnet.gameObject);
    }
}
