using DG.Tweening;

public class IdleState: PlayerState
{
    public IdleState(PlayerAnimatorController animator) : base(animator)
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
