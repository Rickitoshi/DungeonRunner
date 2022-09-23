using UnityEngine;

public class RunState : IState
{
    private readonly PlayerController _player;
    private readonly Animator _animator;

    public RunState(PlayerController player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }
    
    public void Enter()
    {
        _animator.SetTrigger(PlayerController.RUN);
    }

    public void Exit()
    {
    }

    public void Update()
    {
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
    }

    public void FixedUpdate()
    {
        _player.MoveForward();
    }
}
