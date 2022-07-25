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
    private StrafeSide _targetStrafe;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchSide(StrafeSide.Right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchSide(StrafeSide.Left);
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

    private void SwitchSide(StrafeSide strafeSide)
    {
        if (strafeSide == StrafeSide.Left && _targetSidePosition > -StrafeDistance)
        {
            _targetSidePosition -= StrafeDistance;
            _targetStrafe = StrafeSide.Left;
        }
        if (strafeSide == StrafeSide.Right && _targetSidePosition < StrafeDistance)
        {
            _targetSidePosition += StrafeDistance;
            _targetStrafe = StrafeSide.Right;
        }
    }
    
    private void Strafe()
    {
        if (_targetStrafe == StrafeSide.Left)
        {
            _controller.Move(new Vector3(-1, 0, 0) * (StrafeSpeed * Time.deltaTime));
        }

        if (_targetStrafe == StrafeSide.Right)
        {
            _controller.Move(new Vector3(1, 0, 0) * (StrafeSpeed * Time.deltaTime));
        }
    }

    private void MoveForward()
    {
        _controller.Move(Vector3.forward * (MoveSpeed  * Time.deltaTime));
    }
    
    private bool IsReadyToStrafe()
    {
        return Math.Abs(_transform.position.x - _targetSidePosition) > 0;
    }
}
