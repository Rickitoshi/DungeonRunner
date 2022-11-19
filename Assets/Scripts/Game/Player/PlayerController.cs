using DG.Tweening;
using Game.Systems;
using Signals;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(GroundCheckSystem))]
public class PlayerController : MonoBehaviour
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
    private InputHandler _inputHandler;
    private PlayerAnimatorController _animator;
    
    private Vector3 _startPosition;
    private float _targetPositionX;
    private Vector3 _velocity;

    private StateManager _stateManager;
    private State _currentState;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    [Inject]
    private void Construct(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    
    private void Awake()
    {
        _groundCheckSystem = GetComponent<GroundCheckSystem>();
        _animator = GetComponent<PlayerAnimatorController>();

        _stateManager = new StateManager();
        _runState = new RunState(this, _animator, _inputHandler);
        _idleState = new IdleState(this, _animator);
        _failState = new FailState(this, _animator);
    }

    private void Start()
    {
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        _stateManager.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        _stateManager.CurrentState.FixedUpdate();
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
