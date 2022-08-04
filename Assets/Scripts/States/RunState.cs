using UnityEngine;

public class RunState : IState
{
    private PlayerController _player;

    public RunState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        Debug.Log("Enter RunState");
    }

    public void Exit()
    {
        Debug.Log("Exit RunState");
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
