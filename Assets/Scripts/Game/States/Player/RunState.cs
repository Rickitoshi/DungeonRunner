using UnityEngine;

public class RunState : PlayerState
{
    private InputHandler _inputHandler;
    
    public RunState(PlayerController player, Animator animator, InputHandler inputHandler) : base(player, animator)
    {
        _inputHandler = inputHandler;
    }
    
    public override void Enter()
    {
        _animator.SetTrigger(_player.RUN);
    }

    public override void Exit()
    {
        
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

#endif
    }

    public override void  FixedUpdate()
    {
        _player.MoveForward();
        
        if (_player.IsReadyToStrafe())
        {
            _player.Strafe();
        }
    }
}
