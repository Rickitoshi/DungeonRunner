using UnityEngine;

namespace Game.Systems
{
    public class GroundCheckSystem : MonoBehaviour
    {
        private const float MAX_DISTANCE = 1f;
        
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask layerMask;

        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            var position = transform.position;
            Ray ray = new Ray(position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE,layerMask.value))
            {
                var hitPosition = hit.point;
                IsGrounded = Vector3.SqrMagnitude(position - hitPosition) <= groundDistance;
            }
        }
    }
}