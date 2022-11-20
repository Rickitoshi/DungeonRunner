using UnityEngine;

public class FailState: PlayerState
{
    public FailState(PlayerAnimatorController animator) : base(animator)
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