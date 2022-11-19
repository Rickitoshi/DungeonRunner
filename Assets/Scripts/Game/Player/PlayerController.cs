using Game.Systems;
using Signals;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(GroundCheckSystem))]
public class PlayerController : MonoBehaviour, IObstacleVisitor
{
    [SerializeField] private float moveSpeed = 6f ;
    [SerializeField] private float strafeSpeed = 4f ;
    [SerializeField,Space(10f)] private float strafeDistance = 1.5f ;
    
    [SerializeField] private float jumpHeight = 1.3f;
    [SerializeField,Range(0,1)] private float fallForceMultiplier;
    [SerializeField] private float gravityValue = -9.81f;

    public bool IsGrounded => _groundCheckSystem.IsGrounded;

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

    private GroundCheckSystem _groundCheckSystem;
    private SignalBus _signalBus;
    private InputHandler _inputHandler;
    private PlayerAnimatorController _animator;
    
    private CharacterController _controller;
    private Vector3 _startPosition;
    private float _targetPositionX;
    private StrafeDirection _strafeDirection;
    private Vector3 _velocity;

    private StateManager _stateManager;
    private State _currentState;
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
        _groundCheckSystem = GetComponent<GroundCheckSystem>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimatorController>();

        _stateManager = new StateManager();
        _runState = new RunState(this, _animator, _inputHandler);
        _idleState = new IdleState(this, _animator);
        _failState = new FailState(this, _animator);
    }

    private void Start()
    {
        _startPosition = transform.position;
        State = State.Idle;
    }
    
    private void Update()
    {
        _stateManager.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        CheckGround();
        _stateManager.CurrentState.FixedUpdate();
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

    public void Fall()
    {
        if (!_groundCheckSystem.IsGrounded) _velocity.y += gravityValue * fallForceMultiplier;
    }
    
    public void ObstacleVisit(RoadPart roadPart)
    {
        if (_currentState == State.Run)
        {
            _signalBus.Fire(new LoseSignal(roadPart));
        }
    }

    public void SetLobby()
    {
        _controller.Move(_startPosition - transform.position);
        _targetPositionX = 0;
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
