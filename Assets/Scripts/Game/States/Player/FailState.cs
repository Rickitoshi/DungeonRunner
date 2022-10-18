using UnityEngine;

public class FailState: PlayerState
{
    public FailState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        _animator.SetTrigger(_player.DIE);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }
}