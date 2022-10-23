using System;
using Signals;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IObstacleVisitor
{
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

    private SignalBus _signalBus;
    private InputHandler _inputHandler;
    
    private Vector3 _startPosition;
    private CharacterController _controller;
    private float _targetPositionX;
    private StrafeDirection _strafeDirection;
    private Vector3 _velocity;

    private State _currentState;
    private StateManager _stateManager;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    [Inject]
    private void Construct(SignalBus signalBus,InputHandler inputHandler)
    {
        _signalBus = signalBus;
        _inputHandler = inputHandler;
    }
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        ItemsCollector = GetComponent<ItemsCollector>();
        
        _stateManager = new StateManager();
        _runState = new RunState(this, animator, _inputHandler);
        _idleState = new IdleState(this, animator);
        _failState = new FailState(this, animator);
    }

    private void Start()
    {
        _startPosition = transform.position;
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
    
    public bool IsReadyToStrafe()
    {
        if (_strafeDirection == StrafeDirection.Right)
        {
            return transform.position.x - _targetPositionX < 0;  
        }
        else
        {
            return transform.position.x - _targetPositionX > 0;  
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

    public void Visit()
    {
        if (_currentState == State.Run)
        {
            _signalBus.Fire<OnLoseSignal>();
        }
    }

    public void SetDefault()
    {
        _controller.Move(_startPosition - transform.position);
        State = State.Idle;
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
