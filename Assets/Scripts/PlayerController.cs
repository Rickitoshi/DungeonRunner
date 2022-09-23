using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    public static readonly int RUN = Animator.StringToHash("Run");
    public static readonly int IDLE = Animator.StringToHash("Idle");
    
    [Header("Move")]
    [SerializeField] private float moveSpeed = 6f ;
    [SerializeField] private float strafeSpeed = 4f ;
    [SerializeField] private float strafeDistance = 1.5f ;

    [Header("Jump")] 
    [SerializeField] private float jumpHeight = 1.3f;
    [SerializeField] private float gravityValue = -9.81f;

    [Header("Reference")] 
    [SerializeField] private Animator animator;
    
    public bool IsGrounded{ get; private set; }

    public PlayerState State
    {
        get => _currentState;
        set
        {
            if(value==_currentState) return;

            _currentState = value;
            switch (value)
            {
                case PlayerState.Idle:
                    _stateManager.ChangeState(_idleState);
                    break;
                case PlayerState.Run:
                    _stateManager.ChangeState(_runState);
                    break;
            }
        }
    }

    private CharacterController _controller;
    private float _targetSidePosition;
    private StrafeDirection _targetStrafe;
    private Vector3 _velocity;
    
    private PlayerState _currentState;
    private StateManager _stateManager;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        
        _stateManager = new StateManager();
        _runState = new RunState(this, animator);
        _idleState = new IdleState(this, animator);
    }

    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        State = PlayerState.Idle;
    }
    
    private void Update()
    {
        CheckGround();
        _stateManager.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        _stateManager.CurrentState.FixedUpdate();
        Gravity();
        
        if (IsReadyToStrafe())
        {
            Strafe();
        }
    }

    private bool IsReadyToStrafe()
    {
        return Math.Abs(transform.position.x - _targetSidePosition) > 0.04;
    }
    
    private void Strafe()
    {
        if (_targetStrafe == StrafeDirection.Left)
        {
            _controller.Move(Vector3.left * (strafeSpeed * Time.deltaTime));
        }

        if (_targetStrafe == StrafeDirection.Right)
        {
            _controller.Move(Vector3.right * (strafeSpeed * Time.deltaTime));
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
        _velocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
    
    public void SwitchSide(StrafeDirection strafeDirection)
    {
        if (strafeDirection == StrafeDirection.Left && _targetSidePosition > -strafeDistance)
        {
            _targetSidePosition -= strafeDistance;
            _targetStrafe = StrafeDirection.Left;
        }
        if (strafeDirection == StrafeDirection.Right && _targetSidePosition < strafeDistance)
        {
            _targetSidePosition += strafeDistance;
            _targetStrafe = StrafeDirection.Right;
        }
    }
    
    public void MoveForward()
    {
        _controller.Move(Vector3.forward * (moveSpeed  * Time.deltaTime));
    }
    
    public void Jump()
    {
        _velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
    }
}
