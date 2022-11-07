using System.Collections;
using Signals;
using UnityEngine;
using Zenject;

public class ItemsCollector : MonoBehaviour, IItemVisitor
{
    [SerializeField] private float magnetDuration = 5;
    [SerializeField] private float itemMoveSpeed = 9;
    [SerializeField] private Vector3 magnetZone;

    private SignalBus _signalBus;
    private bool _isMagnetActive;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
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
                    visitor.CollectorVisit(this, itemMoveSpeed);
                }
            }
        }
    }

    public void ItemVisit(Coin coin, int cost)
    {
        coin.Deactivate();
        _signalBus.Fire(new CoinsAddSignal(cost));
    }

    public void ItemVisit(Magnet magnet)
    {
        ActivateMagnet();
        magnet.Deactivate();
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
