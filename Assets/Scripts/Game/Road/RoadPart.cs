using System;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    public event Action OnPlayerExit;

    private Item[] _items;

    public void Awake()
    {
        _items = GetComponentsInChildren<Item>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            OnPlayerExit?.Invoke();
        }
    }

    public void ResetItems()
    {
        if (_items.Length > 0)
        {
            foreach (var item in _items)
            {
                item.Activate();
            }
        }
    }
}
