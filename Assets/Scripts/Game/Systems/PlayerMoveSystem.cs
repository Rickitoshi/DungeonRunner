using UnityEngine;
using DG.Tweening;

namespace Game.Systems
{
    [RequireComponent(typeof(GroundCheckSystem))]
    public class PlayerMoveSystem : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f ;
        [Space(10f)]
        [SerializeField] private float strafeDuration = 0.4f ;
        [SerializeField] private float strafeDistance = 1.5f ;
        [Space(10f)]
        [SerializeField] private float jumpHeight = 1.3f;
        [SerializeField,Range(0,1)] private float fallForceMultiplier;
        [Space(10f)]
        [SerializeField] private float gravityValue = -9.81f;

        public bool IsActive;
        public bool IsGrounded => _groundCheckSystem.IsGrounded;
        
        private GroundCheckSystem _groundCheckSystem;
        private Vector3 _startPosition;
        private float _targetPositionX;
        private Vector3 _velocity;

        private void Awake()
        {
            _groundCheckSystem = GetComponent<GroundCheckSystem>();
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if(!IsActive) return;
            
            CheckGround();
            Gravity();
        }
        
        private void CheckGround()
        {
            if (_groundCheckSystem.IsGrounded && _velocity.y < 0)
            {
                _velocity.y = 0f;
            }
        }

        private void Gravity()
        {
            _velocity.y += gravityValue * Time.deltaTime;
            transform.position += _velocity * Time.deltaTime;
        }
        
        private void Strafe()
        {
            transform.DOMoveX(_targetPositionX, strafeDuration);
        }
    
        public void SwitchSide(StrafeDirection strafeDirection)
        {
            switch (strafeDirection)
            {
                case StrafeDirection.Left when _targetPositionX > -strafeDistance:
                    _targetPositionX -= strafeDistance;
                    break;
                case StrafeDirection.Right when _targetPositionX < strafeDistance:
                    _targetPositionX += strafeDistance;
                    break;
            }

            Strafe();
        }

        public void MoveForward()
        {
            transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
        }
    
        public void Jump()
        {
            _velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        public void Fall()
        {
            if (!_groundCheckSystem.IsGrounded) _velocity.y += gravityValue * fallForceMultiplier;
        }

        public void SetDefaultPosition()
        {
            transform.position = _startPosition;
            DOTween.Kill(transform);
            _targetPositionX = 0;
        }
    }
    
    public enum StrafeDirection
    {
        None,
        Left,
        Right
    }
}