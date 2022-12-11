using System;
using System.Collections;
using UnityEngine;

namespace Game.Systems
{
    public class MagnetSystem : MonoBehaviour
    {
        [SerializeField] private float magneticRadius;
        [SerializeField] private float itemMoveSpeed = 9;
        [SerializeField] private LayerMask mask;

        private bool _isMagnetActive;
        private float _magnetDuration;
        private Coroutine _coroutine;
        private Collider[] _colliders;

        private void Awake()
        {
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
                        visitor.MagnetVisit(this, itemMoveSpeed);
                    }
                }
            }
        }
        
        private IEnumerator DeactivateMagnet()
        {
            yield return new WaitForSeconds(_magnetDuration);
            _isMagnetActive = false;
        }

        public void Initialize(float magnetDuration)
        {
            _magnetDuration = magnetDuration;
        }
        
        public void StartMagnetize()
        {
            _isMagnetActive = true;
            _coroutine = StartCoroutine(DeactivateMagnet());
        }
        
        public void StopMagnetize()
        {
            if (_isMagnetActive)
            {
                StopCoroutine(_coroutine);
                _isMagnetActive = false;
            }
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, magneticRadius);
        }
    }
}