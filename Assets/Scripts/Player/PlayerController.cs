using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour, IObstacleVisitor
{
    public static PlayerController Instance { get; private set; }
    
    public readonly int RUN = Animator.StringToHash("Run");
    public readonly int IDLE = Animator.StringToHash("Idle");
    public readonly int DIE = Animator.StringToHash("Die");
    
    [Header("Move")]
    [SerializeField] private float moveSpeed = 6f ;
    [SerializeField] private float strafeSpeed = 4f ;
    [SerializeField] private float strafeDistance = 1.5f ;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.3f;
    [SerializeField] private float gravityValue = -9.81f;

    [Header("Reference")] 
    [SerializeField] private Animator animator;

    public event Action OnDie;
    public bool IsGrounded{ get; private set; }
    public ItemsCollector ItemsCollector { get; private set; }

    public State State
    {
        get => _currentState;
        set
        {
            if(value==_currentState) return;

            _currentState = value;
            switch (value)
            {
                case State.Idle:
                    _stateManager.ChangeState(_idleState);
                    break;
                case State.Run:
                    _stateManager.ChangeState(_runState);
                    break;
                case State.Die:
                    _stateManager.ChangeState(_failState);
                    break;
            }
        }
    }

    private CharacterController _controller;
    private float _targetPositionX;
    private StrafeDirection _strafeDirection;
    private Vector3 _velocity;

    private State _currentState;
    private StateManager _stateManager;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        _controller = GetComponent<CharacterController>();
        ItemsCollector = GetComponent<ItemsCollector>();
        
        _stateManager = new StateManager();
        _runState = new RunState(this, animator);
        _idleState = new IdleState(this, animator);
        _failState = new FailState(this, animator);
    }

    private void Start()
    {
        State = State.Idle;
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
    }

    public bool IsReadyToStrafe()
    {
        return Math.Abs(transform.position.x - _targetPositionX) > 0.03f;
    }
    
    public void Strafe()
    {
        if (_strafeDirection == StrafeDirection.Left)
        {
            _controller.Move(Vector3.left * (strafeSpeed * Time.deltaTime));
        }
        else
        {
            _controller.Move(Vector3.right * (strafeSpeed * Time.deltaTime));
        }
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
        _strafeDirection = strafeDirection;
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

    public void MoveForward()
    {
        _controller.Move(Vector3.forward * (moveSpeed  * Time.deltaTime));
    }
    
    public void Jump()
    {
        _velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
    }

    public void Visit()
    {
        OnDie?.Invoke();
    }
}

public enum State
{
    None,
    Run,
    Idle,
    Die
}

public enum StrafeDirection
{
    None,
    Left,
    Right
}
