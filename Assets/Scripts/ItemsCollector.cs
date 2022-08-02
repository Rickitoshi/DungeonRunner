using System.Collections;
using UnityEngine;

public class ItemsCollector : MonoBehaviour, IItemVisitor
{
    [SerializeField] private float MagnetDuration = 5;
    [SerializeField] private float ItemMoveSpeed = 5;
    [SerializeField] private Vector3 MagnetZone;

    private bool _isMagnetActive;
    
    public void Visit(Coin coin, int cost)
    {
        print(cost);
        coin.gameObject.SetActive(false);
    }

    public void Visit(Magnet magnet)
    {
        ActivateMagnet();
        magnet.gameObject.SetActive(false);
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
        Collider[] entities = Physics.OverlapBox(transform.position, MagnetZone);
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out IItemCollectorVisitor visitor))
                {
                    visitor.Visit(this, ItemMoveSpeed);
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
        yield return new WaitForSeconds(MagnetDuration);
        _isMagnetActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, MagnetZone);
    }
}
