using UnityEngine;

public class IdleState: PlayerState
{
    public IdleState(PlayerController player, PlayerAnimatorController animator) : base(player, animator)
    {
        
    }
    
    public override void Enter()
    {
        _animator.SetIdle();
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
