using DG.Tweening;
using Game.Systems;
using UnityEngine;


public class RunState : BasePlayerState
{
    private readonly InputHandler _inputHandler;
    private readonly ParticlesManager _particlesManager;

    public RunState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator, InputHandler inputHandler,ParticlesManager particlesManager) : base(moveSystem, animator)
    {
        _inputHandler = inputHandler;
        _particlesManager = particlesManager;
    }
    
    public override void Enter()
    {
        Animator.SetRun();
        _particlesManager.SetActiveRunParticle(true);
        MoveSystem.IsActive = true;
    }

    public override void Exit()
    {
        _particlesManager.SetActiveRunParticle(false);
    }

    public override void Update()
    {
        if (_inputHandler.RightSwipe)
        {
            MoveSystem.SwitchSide(StrafeDirection.Right);
        }

        if (_inputHandler.LeftSwipe)
        {
            MoveSystem.SwitchSide(StrafeDirection.Left);
        }

        if (_inputHandler.UpSwipe && MoveSystem.IsGrounded)
        {
            MoveSystem.Jump();
        }

        if (_inputHandler.DownSwipe && !MoveSystem.IsGrounded)
        {
            MoveSystem.Fall();
        }
    }

    public override void  FixedUpdate()
    {
        MoveSystem.MoveForward();
        _particlesManager.SetActiveRunParticle(MoveSystem.IsGrounded);
    }
}
