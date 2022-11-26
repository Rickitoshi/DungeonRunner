using System;
using System.Collections;
using Signals;
using UnityEngine;
using Zenject;

public class ItemsCollector : MonoBehaviour, IItemVisitor
{
    [SerializeField] private float magneticRadius;
    [SerializeField] private LayerMask mask;
    
    private SignalBus _signalBus;
    private ParticlesManager _particlesManager;
    
    private bool _isMagnetActive;
    private float _magnetDuration;
    private float _itemMoveSpeed;
    private Collider[] _colliders;

    [Inject]
    private void Construct(SignalBus signalBus, ParticlesManager particlesManager)
    {
        _signalBus = signalBus;
        _particlesManager = particlesManager;
        _colliders = new Collider[3];
    }

    private void FixedUpdate()
    {
        if (!_isMagnetActive) return;

        PullItem();
    }

    private void PullItem()
    {
        int count= Physics.OverlapSphereNonAlloc(transform.position, magneticRadius, _colliders, mask);
        if (count > 0)
        {
            for (var i = 0; i < count; i++)
            {
                var entity = _colliders[i];
                if (entity.TryGetComponent(out IItemMagnetVisitor visitor))
                {
                    visitor.MagnetVisit(this, _itemMoveSpeed);
                }
            }
        }
    }

    public void ItemVisit(Coin coin)
    {
        _particlesManager.SetPickUpItemParticle(coin.transform.position);
        coin.Deactivate();
        _signalBus.Fire(new CoinsPickUpSignal(coin.Value));
    }

    public void ItemVisit(Magnet magnet)
    {
        _particlesManager.SetPickUpItemParticle(magnet.transform.position);
        _magnetDuration = magnet.Duration;
        _itemMoveSpeed = magnet.MagnetizationRate;
        _signalBus.Fire(new MagnetSignal(_magnetDuration));
        magnet.Deactivate();
        ActivateMagnet();
    }
    
    private void ActivateMagnet()
    {
        _isMagnetActive = true;
        StartCoroutine(DeactivateMagnet());
    }

    private IEnumerator DeactivateMagnet()
    {
        yield return new WaitForSeconds(_magnetDuration);
        _isMagnetActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, magneticRadius);
    }
}
