using UnityEngine;

public class FailState: PlayerState
{
    public FailState(PlayerController player, PlayerAnimatorController animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        _animator.SetDie();
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