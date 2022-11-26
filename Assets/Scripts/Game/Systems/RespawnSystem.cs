using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class RespawnSystem : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private float duration = 2;
        [SerializeField] private float decline = 1;
        [SerializeField] private Vector3 particleOffsetAlive;
        [SerializeField] private Vector3 particleOffsetDead;

        [Inject] private ParticlesManager _particlesManager;

        public event Action<RespawnPhaseEnded> OnRespawnPhaseEnded;

        public void BeginRespawn(bool isDead = false)
        {
            var point = transform.position;
            _particlesManager.SetSpawnParticle(point, isDead ?particleOffsetDead  : particleOffsetAlive);
            transform.DOMoveY(point.y - decline, duration).OnComplete(() =>
            {
                OnRespawnPhaseEnded?.Invoke(RespawnPhaseEnded.Begin);
                FinishRespawn();
            });
        }

        private void FinishRespawn()
        {
            var point = respawnPoint.position;
            transform.position = new Vector3(point.x, point.y - decline, point.z);
            _particlesManager.SetSpawnParticle(point, particleOffsetAlive);
            transform.DOMoveY(point.y, duration).OnComplete(() =>
            {
                OnRespawnPhaseEnded?.Invoke(RespawnPhaseEnded.Finish);
            });
        }
    }

    public enum RespawnPhaseEnded
    {
        Begin,
        Finish
    }
}