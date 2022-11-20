using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItemMagnetVisitor
{
    public bool IsActive => gameObject.activeSelf;
    public bool IsMagnetized { get; private set; }
    
    private ItemsCollector _target;
    private float _moveSpeed;
    
    public void MagnetVisit(ItemsCollector collector,float moveSpeed)
    {
        if (!IsMagnetized)
        {
            IsMagnetized = true;
            _target = collector;
            _moveSpeed = moveSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (IsMagnetized)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IItemVisitor itemVisitor))
        {
            Visit(itemVisitor);
            IsMagnetized = false;
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
