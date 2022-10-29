using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItemCollectorVisitor
{
    private bool _isReadyMove;
    private ItemsCollector _target;
    private float _moveSpeed;
    
    public void CollectorVisit(ItemsCollector collector,float moveSpeed)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IItemVisitor itemVisitor))
        {
            Visit(itemVisitor);
            _isReadyMove = false;
        }
    }
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
    protected abstract void Visit(IItemVisitor itemVisitor);

}
