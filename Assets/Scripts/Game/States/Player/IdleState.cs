using DG.Tweening;
using Game.Systems;

public class IdleState: BasePlayerState
{
    public IdleState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator) : base(moveSystem, animator)
    {
    }
    
    public override void Enter()
    {
        Animator.SetIdle();
        MoveSystem.IsActive = false;
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
