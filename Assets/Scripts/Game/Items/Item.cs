using System;
using Game.Systems;
using UnityEngine;

public abstract class Item : MonoBehaviour, IItemMagnetVisitor
{
    public bool IsActive => gameObject.activeSelf;
    public bool IsMagnetized { get; private set; }
    
    private MagnetSystem _target;
    private float _moveSpeed;
    private Vector3 _defaultPosition;
    
    public void MagnetVisit(MagnetSystem magnet,float moveSpeed)
    {
        if (!IsMagnetized)
        {
            IsMagnetized = true;
            _target = magnet;
            _moveSpeed = moveSpeed;
            _defaultPosition = transform.localPosition;
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
        if (_defaultPosition != Vector3.zero)
        {
            transform.localPosition = _defaultPosition;
        }
        gameObject.SetActive(true);
    }
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
    protected abstract void Visit(IItemVisitor itemVisitor);

}
