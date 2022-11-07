using System;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    public event Action OnPlayerExit;

    private Item[] _items;
    private Obstacle[] _obstacles;

    public void Awake()
    {
        _items = GetComponentsInChildren<Item>();
        _obstacles = GetComponentsInChildren<Obstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            OnPlayerExit?.Invoke();
        }
    }

    public void DeactivateItemsAndObstacles()
    {
        if (_obstacles.Length > 0)
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.Deactivate();
            }
        }
        
        if (_items.Length > 0)
        {
            foreach (var item in _items)
            {
                if (item.IsActive)
                {
                    item.Deactivate();
                }
            }
        }
    }
    
    public void ResetItemsAndObstacles()
    {
        if (_items.Length > 0)
        {
            foreach (var item in _items)
            {
                if (!item.IsActive)
                {
                    item.Activate();
                }
            }
        }
        if (_obstacles.Length > 0)
        {
            foreach (var obstacle in _obstacles)
            {
                if (!obstacle.IsActive)
                {
                    obstacle.Activate();
                }
            }
        }
    }
}
