using UnityEngine;

public abstract class Item : MonoBehaviour, IItemCollectorVisitor
{
    [SerializeField] private float PickUpRadius = 1f;

    private bool _isReadyMove;
    private ItemsCollector _target;
    private float _moveSpeed;
    
    public void Visit(ItemsCollector collector,float moveSpeed)
    {
        if (!_isReadyMove)
        {
            _isReadyMove = true;
            _target = collector;
            _moveSpeed = moveSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_isReadyMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        }
    }

    protected bool isVisitorDetected(out IItemVisitor itemVisitor )
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, PickUpRadius);
        if (entities.Length > 0)
        {
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out itemVisitor))
                {
                    _isReadyMove = false;
                    return true;
                }
            }
        }

        itemVisitor = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PickUpRadius);
    }
    
}
