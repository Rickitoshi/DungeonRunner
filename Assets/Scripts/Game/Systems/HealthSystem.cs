using UnityEngine;
using Zenject;
using Signals;

namespace Game.Systems
{
    public class HealthSystem : MonoBehaviour,IObstacleVisitor
    {
        [SerializeField, Min(1)] private int maxHealth = 1;

        [Inject]
        private SignalBus _signalBus;
        
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void ObstacleVisit(RoadPart roadPart)
        {
            _signalBus.Fire(new LoseSignal(roadPart));
        }
    }
}