using Game.Systems;
using UnityEngine;

public class DeadState: BasePlayerState
{
    public DeadState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator) : base(moveSystem, animator)
    {
    }

    public override void Enter()
    {
        Animator.SetDie();
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