using System;
using Game.Systems;
using Signals;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthSystem),typeof(RespawnSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveSystem playerMoveSystem;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerAnimatorController animatorController;
    [SerializeField] private RespawnSystem respawnSystem;

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
                    _stateManager.ChangeState(_deadState);
                    break;
            }
        }
    }
    
    private InputHandler _inputHandler;
    private SignalBus _signalBus;
    private ParticlesManager _particlesManager;
    
    private StateManager _stateManager;
    private State _currentState;
    private RunState _runState;
    private IdleState _idleState;
    private DeadState _deadState;

    [Inject]
    private void Construct(InputHandler inputHandler,SignalBus signalBus,ParticlesManager particlesManager)
    {
        _inputHandler = inputHandler;
        _signalBus = signalBus;
        _particlesManager = particlesManager;
    }
    
    private void Awake()
    {
        Subscribe();
        
        _stateManager = new StateManager();
        _runState = new RunState(playerMoveSystem, animatorController, _inputHandler,_particlesManager);
        _idleState = new IdleState(playerMoveSystem, animatorController);
        _deadState = new DeadState(playerMoveSystem, animatorController);
    }

    private void Start()
    {
        _particlesManager.GetRunParticle(transform);
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
        respawnSystem.OnRespawnPhaseEnded += OnRespawnPhaseEnded;
    }  
    
    private void Unsubscribe()
    {
        healthSystem.OnDie -= OnHealthEnd;
        respawnSystem.OnRespawnPhaseEnded -= OnRespawnPhaseEnded;
    }

    private void OnHealthEnd()
    {
        _signalBus.Fire<PlayerDieSignal>();
    }

    private void OnRespawnPhaseEnded(RespawnPhaseEnded phaseEnded)
    {
        _signalBus.Fire(new PlayerRespawnPhaseEndedSignal(phaseEnded));
    }

    public void Respawn()
    {
        healthSystem.Reset();
        playerMoveSystem.ResetBehaviour();
        
        if (State == State.Die)
        {
            respawnSystem.BeginRespawn();
        }
        else
        {
            respawnSystem.InstantRespawn();
        }
       
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
