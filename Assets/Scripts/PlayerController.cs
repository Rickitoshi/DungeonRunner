using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f ;
    [SerializeField] private float StrafeSpeed = 1f ;
    [SerializeField] private float StrafeDistance = 1f ;

    private CharacterController _controller;
    private Transform _transform;
    private float _targetSidePosition;
    private StrafeDirection _targetStrafe;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchSide(StrafeDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchSide(StrafeDirection.Left);
        }
    }

    private void FixedUpdate()
    {
        MoveForward();

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

    private void MoveForward()
    {
        _controller.Move(Vector3.forward * (MoveSpeed  * Time.deltaTime));
    }
    
    private bool IsReadyToStrafe()
    {
        return Math.Abs(_transform.position.x - _targetSidePosition) > 0.04;
    }
}
