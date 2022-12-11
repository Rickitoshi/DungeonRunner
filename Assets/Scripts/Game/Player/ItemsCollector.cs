using Signals;
using UnityEngine;
using Zenject;

public class ItemsCollector : MonoBehaviour, IItemVisitor
{
    private SignalBus _signalBus;
    private ParticlesManager _particlesManager;

    [Inject]
    private void Construct(SignalBus signalBus, ParticlesManager particlesManager)
    {
        _signalBus = signalBus;
        _particlesManager = particlesManager;
    }
    
    public void ItemVisit(CoinItem coinItem)
    {
        coinItem.Deactivate();
        _particlesManager.SetPickUpItemParticle(coinItem.transform.position);
        _signalBus.Fire(new CoinsPickUpSignal(coinItem.Value));
    }
    
    public void ItemVisit(MagnetItem magnetItem)
    {
        magnetItem.Deactivate();
        _particlesManager.SetPickUpItemParticle(magnetItem.transform.position);
        _signalBus.Fire<MagnetSignal>();
    }
}
