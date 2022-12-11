using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems
{
    public class HealthSystem : MonoBehaviour,IObstacleVisitor
    {
        public event Action OnDie;

        private int _maxHealth;
        private int _currentHealth;
        
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

        public void Initialize(int maxHealth)
        {
            _maxHealth = maxHealth;
            Reset();
        }
        
        public void Reset()
        {
            _currentHealth = _maxHealth;
        }
    }
}