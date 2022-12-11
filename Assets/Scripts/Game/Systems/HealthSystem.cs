using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems
{
    public class HealthSystem : MonoBehaviour,IObstacleVisitor
    {
        [SerializeField, Min(1)] private int maxHealth = 1;

        public event Action OnDie;

        private int _currentHealth;
        
        private void Start()
        {
            Reset();
        }
        
        private void GetDamage(int value)
        {
            _currentHealth = -value;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        
        [ContextMenu("Die")]
        private void Die()
        {
            _currentHealth = 0;
            OnDie?.Invoke();
        }
        
        public void ObstacleVisit(int damage)
        {
            if (_currentHealth > 0) GetDamage(damage);
        }

        public void Reset()
        {
            _currentHealth = maxHealth;
        }
    }
}