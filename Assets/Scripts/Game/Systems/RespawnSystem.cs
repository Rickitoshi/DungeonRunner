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
        [SerializeField] private float declineAlive = 1.5f;
        [SerializeField] private float declineDead = 1.5f;
        [SerializeField] private Vector3 particleOffsetAlive;
        [SerializeField] private Vector3 particleOffsetDead;

        [Inject] private ParticlesManager _particlesManager;

        public event Action<RespawnPhaseEnded> OnRespawnPhaseEnded;

        public void InstantRespawn()
        {
            transform.position = respawnPoint.position;
            OnRespawnPhaseEnded?.Invoke(RespawnPhaseEnded.Begin);
            OnRespawnPhaseEnded?.Invoke(RespawnPhaseEnded.Finish);
        }
        
        public void BeginRespawn()
        {
            var point = transform.position;
            _particlesManager.SetSpawnParticle(point, particleOffsetDead);
            transform.DOMoveY(point.y - declineDead, duration).OnComplete(() =>
            {
                OnRespawnPhaseEnded?.Invoke(RespawnPhaseEnded.Begin);
                FinishRespawn();
            });
        }

        private void FinishRespawn()
        {
            var point = respawnPoint.position;
            transform.position = new Vector3(point.x, point.y - declineAlive, point.z);
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