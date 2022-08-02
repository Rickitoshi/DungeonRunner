using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItemCollectorVisitor
{
    [SerializeField] private float PickUpRadius = 1f;

    private bool _isReadyMove;
    private Vector3 _target;
    private float _moveSpeed;
    
    public void Visit(ItemsCollector collector,float moveSpeed)
    {
        _isReadyMove = true;
        _target = collector.transform.position;
        _moveSpeed = moveSpeed;
    }

    protected virtual void FixedUpdate()
    {
        if (_isReadyMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed * Time.deltaTime);
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
