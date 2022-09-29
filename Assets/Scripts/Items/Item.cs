using UnityEngine;

public abstract class Item : MonoBehaviour, IItemCollectorVisitor
{
    [SerializeField] private float PickUpRadius = 1f;

    private Collider[] _overlapColliders = new Collider[4];
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
        SearchVisitor();
    }

    private void SearchVisitor()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, PickUpRadius, _overlapColliders);
        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IItemVisitor itemVisitor))
                {
                    Visit(itemVisitor);
                    _isReadyMove = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PickUpRadius);
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
