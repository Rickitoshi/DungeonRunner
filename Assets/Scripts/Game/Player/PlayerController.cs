using Game.Player;
using Game.Player.Stats;
using Game.Systems;
using Signals;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthSystem),typeof(RespawnSystem),typeof(ItemsCollector))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveSystem playerMoveSystem;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private MagnetSystem magnetSystem;
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
    private PlayerConfig _config;
    private StatsContainer _statsContainer;

    private StateManager _stateManager;
    private State _currentState;
    private RunState _runState;
    private IdleState _idleState;
    private DeadState _deadState;

    [Inject]
    private void Construct(InputHandler inputHandler,SignalBus signalBus,ParticlesManager particlesManager,PlayerConfig config, StatsContainer container)
    {
        _inputHandler = inputHandler;
        _signalBus = signalBus;
        _particlesManager = particlesManager;
        _config = config;
        _statsContainer = container;
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
        
        playerMoveSystem.Initialize(_config.MoveSpeed,_config.StrafeDuration,_config.StrafeDistance,_config.JumpHeight);
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
        _signalBus.Subscribe<MagnetSignal>(ActivateMagnet);
        _signalBus.Subscribe<BackToLobbySignal>(DeactivateMagnet);
    }  
    
    private void Unsubscribe()
    {
        healthSystem.OnDie -= OnHealthEnd;
        respawnSystem.OnRespawnPhaseEnded -= OnRespawnPhaseEnded;
        _signalBus.Unsubscribe<MagnetSignal>(ActivateMagnet);
        _signalBus.Unsubscribe<BackToLobbySignal>(DeactivateMagnet);
    }

    private void ActivateMagnet()
    {
        magnetSystem.StartMagnetize();
    }

    private void DeactivateMagnet()
    {
        magnetSystem.StopMagnetize();
    }
    
    private void OnHealthEnd()
    {
        DeactivateMagnet();
        _signalBus.Fire<PlayerDieSignal>();
    }

    private void OnRespawnPhaseEnded(RespawnPhaseEnded phaseEnded)
    {
        _signalBus.Fire(new PlayerRespawnPhaseEndedSignal(phaseEnded));
    }

    public void UpdateStats()
    {
        healthSystem.Initialize(_statsContainer.Health);
        magnetSystem.Initialize(_statsContainer.Magnet);
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
