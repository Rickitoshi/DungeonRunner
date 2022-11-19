using DG.Tweening;
using UnityEngine;

public class RunState : PlayerState
{
    private InputHandler _inputHandler;
    
    public RunState(PlayerController player, PlayerAnimatorController animator, InputHandler inputHandler) : base(player, animator)
    {
        _inputHandler = inputHandler;
    }
    
    public override void Enter()
    {
        _animator.SetRun();
        DOTween.Play(_player.gameObject.transform);
    }

    public override void Exit()
    {
        DOTween.Pause(_player.gameObject.transform);
    }

    public override void Update()
    {
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _player.SwitchSide(StrafeDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.SwitchSide(StrafeDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _player.IsGrounded)
        {
            _player.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_player.IsGrounded)
        {
            _player.Fall();
        }
        
#elif UNITY_ANDROID

        if (_inputHandler.RightSwipe)
        {
            _player.SwitchSide(StrafeDirection.Right);
        }

        if (_inputHandler.LeftSwipe)
        {
            _player.SwitchSide(StrafeDirection.Left);
        }

        if (_inputHandler.UpSwipe && _player.IsGrounded)
        {
            _player.Jump();
        }

        if (_inputHandler.DownSwipe && !_player.IsGrounded)
        {
            _player.Fall();
        }

#endif
    }

    public override void  FixedUpdate()
    {
        _player.MoveForward();
    }
}
