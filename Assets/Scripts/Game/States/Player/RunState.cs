using DG.Tweening;
using Game.Systems;
using UnityEngine;


public class RunState : BasePlayerState
{
    private readonly InputHandler _inputHandler;

    public RunState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator, InputHandler inputHandler) : base(moveSystem, animator)
    {
        _inputHandler = inputHandler;
    }
    
    public override void Enter()
    {
        Animator.SetRun();
        MoveSystem.IsActive = true;
    }

    public override void Exit()
    {
      
    }

    public override void Update()
    {
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveSystem.SwitchSide(StrafeDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveSystem.SwitchSide(StrafeDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.Space) && MoveSystem.IsGrounded)
        {
            MoveSystem.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !MoveSystem.IsGrounded)
        {
            MoveSystem.Fall();
        }
        
#elif UNITY_ANDROID

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

#endif
    }

    public override void  FixedUpdate()
    {
        MoveSystem.MoveForward();
    }
}
