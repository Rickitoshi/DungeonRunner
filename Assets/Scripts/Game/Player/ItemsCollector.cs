using System;
using System.Collections;
using UnityEngine;

public class ItemsCollector : MonoBehaviour, IItemVisitor
{
    [SerializeField] private float magnetDuration = 5;
    [SerializeField] private float itemMoveSpeed = 9;
    [SerializeField] private Vector3 magnetZone;

    public event Action<int> OnPickUpCoin;

    private bool _isMagnetActive;
    
    public void Visit(Coin coin, int cost)
    {
        coin.Deactivate();
        OnPickUpCoin?.Invoke(cost);
    }

    public void Visit(Magnet magnet)
    {
        ActivateMagnet();
        magnet.Deactivate();
    }

    private void FixedUpdate()
    {
        if (_isMagnetActive)
        {
            SearchVisitor();
        }
    }

    private void SearchVisitor()
    {
        Collider[] entities = Physics.OverlapBox(transform.position, magnetZone);
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out IItemCollectorVisitor visitor))
                {
                    visitor.Visit(this, itemMoveSpeed);
                }
            }
        }
    }

    private void ActivateMagnet()
    {
        _isMagnetActive = true;
        StartCoroutine(DeactivateMagnet());
    }

    private IEnumerator DeactivateMagnet()
    {
        yield return new WaitForSeconds(magnetDuration);
        _isMagnetActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, magnetZone);
    }
}
