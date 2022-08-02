using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float MoveSpeed = 1f ;
    [SerializeField] private float StrafeSpeed = 1f ;
    [SerializeField] private float StrafeDistance = 1f ;

    [Header("Jump")] 
    [SerializeField] private float JumpHeight = 1;
    [SerializeField] private float GravityValue = -9.81f;

    private CharacterController _controller;
    private Transform _transform;
    private float _targetSidePosition;
    private StrafeDirection _targetStrafe;
    private bool _isGrounded;
    private Vector3 _velocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    
    private void Update()
    {
        CheckGround();
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchSide(StrafeDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchSide(StrafeDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
        
    }

    private void FixedUpdate()
    {
        MoveForward();
        Gravity();

        if (IsReadyToStrafe())
        {
            Strafe();
        }
        
    }

    private void SwitchSide(StrafeDirection strafeDirection)
    {
        if (strafeDirection == StrafeDirection.Left && _targetSidePosition > -StrafeDistance)
        {
            _targetSidePosition -= StrafeDistance;
            _targetStrafe = StrafeDirection.Left;
        }
        if (strafeDirection == StrafeDirection.Right && _targetSidePosition < StrafeDistance)
        {
            _targetSidePosition += StrafeDistance;
            _targetStrafe = StrafeDirection.Right;
        }
    }
    
    private void Strafe()
    {
        if (_targetStrafe == StrafeDirection.Left)
        {
            _controller.Move(Vector3.left * (StrafeSpeed * Time.deltaTime));
        }

        if (_targetStrafe == StrafeDirection.Right)
        {
            _controller.Move(Vector3.right * (StrafeSpeed * Time.deltaTime));
        }
    }

    private void CheckGround()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }

    private void Jump()
    {
        _velocity.y += Mathf.Sqrt(JumpHeight * -2.0f * GravityValue);
    }

    private void Gravity()
    {
        _velocity.y += GravityValue * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void MoveForward()
    {
        _controller.Move(Vector3.forward * (MoveSpeed  * Time.deltaTime));
    }
    
    private bool IsReadyToStrafe()
    {
        return Math.Abs(_transform.position.x - _targetSidePosition) > 0.04;
    }
}
