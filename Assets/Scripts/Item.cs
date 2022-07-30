using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float PickUpRadius = 1f;
    
    protected bool isCollectorDetected(out IItemsCollector itemsCollector )
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, PickUpRadius);
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out itemsCollector))
                {
                    return true;
                }
            }
        }

        itemsCollector = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PickUpRadius);
    }
}
