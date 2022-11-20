using DG.Tweening;
using Game.Systems;
using UnityEngine;


public class RunState : PlayerState
{
    private readonly InputHandler _inputHandler;
    private readonly PlayerMoveSystem _moveSystem;
    
    public RunState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator, InputHandler inputHandler) : base(animator)
    {
        _inputHandler = inputHandler;
        _moveSystem = moveSystem;
    }
    
    public override void Enter()
    {
        _animator.SetRun();
        _moveSystem.IsActive = true;
        DOTween.Play(_moveSystem.gameObject.transform);
    }

    public override void Exit()
    {
        _moveSystem.IsActive = false;
        DOTween.Pause(_moveSystem.gameObject.transform);
    }

    public override void Update()
    {
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveSystem.SwitchSide(StrafeDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveSystem.SwitchSide(StrafeDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _moveSystem.IsGrounded)
        {
            _moveSystem.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_moveSystem.IsGrounded)
        {
            _moveSystem.Fall();
        }
        
#elif UNITY_ANDROID

        if (_inputHandler.RightSwipe)
        {
            _moveSystem.SwitchSide(StrafeDirection.Right);
        }

        if (_inputHandler.LeftSwipe)
        {
            _moveSystem.SwitchSide(StrafeDirection.Left);
        }

        if (_inputHandler.UpSwipe && _moveSystem.IsGrounded)
        {
            _moveSystem.Jump();
        }

        if (_inputHandler.DownSwipe && !_moveSystem.IsGrounded)
        {
            _moveSystem.Fall();
        }

#endif
    }

    public override void  FixedUpdate()
    {
        _moveSystem.MoveForward();
    }
}
