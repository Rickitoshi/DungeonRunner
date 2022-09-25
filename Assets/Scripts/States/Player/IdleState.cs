using UnityEngine;

public class IdleState: PlayerState
{
    public IdleState(PlayerController player, Animator animator) : base(player, animator)
    {
        
    }
    
    public override void Enter()
    {
        _animator.SetTrigger(_player.IDLE);
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
