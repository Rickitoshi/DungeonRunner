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
    
    public bool IsGrounded{ get; private set; }

    private CharacterController _controller;
    private float _targetSidePosition;
    private StrafeDirection _targetStrafe;
    private Vector3 _velocity;
    private StateManager _stateManager;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stateManager = new StateManager();
        _runState = new RunState(this);
        _idleState = new IdleState();
        _failState = new FailState();
    }

    private void Start()
    {
        _stateManager.Initialize(_runState);
    }


    private void Update()
    {
        CheckGround();
        _stateManager.CurrentState.Update();
    }

    public void FixedUpdate()
    {
        _stateManager.CurrentState.FixedUpdate();
        Gravity();
        
        if (IsReadyToStrafe())
        {
            Strafe();
        }
    }

    public void SwitchSide(StrafeDirection strafeDirection)
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
    
    public void MoveForward()
    {
        _controller.Move(Vector3.forward * (MoveSpeed  * Time.deltaTime));
    }
    
    public void Jump()
    {
        _velocity.y += Mathf.Sqrt(JumpHeight * -2.0f * GravityValue);
    }
    
    private bool IsReadyToStrafe()
    {
        return Math.Abs(transform.position.x - _targetSidePosition) > 0.04;
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
        IsGrounded = _controller.isGrounded;
        if (IsGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }

    private void Gravity()
    {
        _velocity.y += GravityValue * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
