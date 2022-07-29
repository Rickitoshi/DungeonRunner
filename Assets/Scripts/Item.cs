using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float PickUpRadius;
    
    protected bool isCollectorDetected(out IItemsCollector ItemsCollector )
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, PickUpRadius);
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out IItemsCollector itemsCollector))
                {
                    ItemsCollector = itemsCollector;
                    return true;
                }
            }
        }

        ItemsCollector = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PickUpRadius);
    }
}
