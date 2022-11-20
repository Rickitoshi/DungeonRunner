using Game.Systems;
using Signals;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveSystem playerMoveSystem;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerAnimatorController animatorController;

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
    
    private InputHandler _inputHandler;
    private SignalBus _signalBus;
    
    private StateManager _stateManager;
    private State _currentState;
    private RunState _runState;
    private IdleState _idleState;
    private FailState _failState;

    [Inject]
    private void Construct(InputHandler inputHandler,SignalBus signalBus)
    {
        _inputHandler = inputHandler;
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        Subscribe();
        
        _stateManager = new StateManager();
        _runState = new RunState(playerMoveSystem, animatorController, _inputHandler);
        _idleState = new IdleState(animatorController);
        _failState = new FailState(animatorController);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Update()
    {
        _stateManager.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        _stateManager.CurrentState.FixedUpdate();
    }

    private void Subscribe()
    {
        healthSystem.OnDie += OnHealthEnd;
    }  
    
    private void Unsubscribe()
    {
        healthSystem.OnDie -= OnHealthEnd;
    }

    private void OnHealthEnd()
    {
        _signalBus.Fire<LoseSignal>();
    }

    public void SetDefault()
    {
        playerMoveSystem.SetDefaultPosition();
        Relive();
    }

    public void Relive()
    {
        healthSystem.Reset();
    }
}

public enum State
{
    None,
    Run,
    Idle,
    Die
}
