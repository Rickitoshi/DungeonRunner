using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController player, Animator animator) : base(player, animator)
    {
        
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
        if (_player.IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _player.SwitchSide(StrafeDirection.Right);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _player.SwitchSide(StrafeDirection.Left);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.Jump();
            }
        }
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
