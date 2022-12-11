using System;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Game.Systems
{
    [RequireComponent(typeof(GroundCheckSystem))]
    public class PlayerMoveSystem : MonoBehaviour
    {
        [SerializeField,Range(0,1)] private float fallForceMultiplier;
        [Space(10f)]
        [SerializeField] private float gravityValue = -9.81f;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == _isActive) return;

                _isActive = value;
                if (_isActive)
                {
                    DOTween.Play(transform);
                }
                else
                {
                    DOTween.Pause(transform);
                }
            }
        }

        public bool IsGrounded => _groundCheckSystem.IsGrounded;

        private float _moveSpeed ;
        private float _strafeDuration ;
        private float _strafeDistance ;
        private float _jumpHeight ;
        
        private GroundCheckSystem _groundCheckSystem;
        private Vector3 _startPosition;
        private float _targetPositionX;
        private Vector3 _velocity;
        private bool _isActive;

        private void Awake()
        {
            _groundCheckSystem = GetComponent<GroundCheckSystem>();
        }

        private void FixedUpdate()
        {
            Gravity();
            CheckGround();
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
            transform.DOMoveX(_targetPositionX, _strafeDuration);
        }
    
        public void SwitchSide(StrafeDirection strafeDirection)
        {
            switch (strafeDirection)
            {
                case StrafeDirection.Left when _targetPositionX > -_strafeDistance:
                    _targetPositionX -= _strafeDistance;
                    break;
                case StrafeDirection.Right when _targetPositionX < _strafeDistance:
                    _targetPositionX += _strafeDistance;
                    break;
            }

            Strafe();
        }

        public void Initialize(float moveSpeed, float strafeDuration, float strafeDistance, float jumpHeight)
        {
            _moveSpeed = moveSpeed;
            _strafeDuration = strafeDuration;
            _strafeDistance = strafeDistance;
            _jumpHeight = jumpHeight;
        }
        
        public void MoveForward()
        {
            transform.position += Vector3.forward * (_moveSpeed * Time.deltaTime);
        }
    
        public void Jump()
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * gravityValue);
        }

        public void Fall()
        {
            if (!_groundCheckSystem.IsGrounded) _velocity.y += gravityValue * fallForceMultiplier;
        }

        public void ResetBehaviour()
        {
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