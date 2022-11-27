using Game.Systems;
using UnityEngine;

public abstract class BasePlayerState
{
    protected readonly PlayerAnimatorController Animator;
    protected readonly PlayerMoveSystem MoveSystem;

    protected BasePlayerState(PlayerMoveSystem moveSystem, PlayerAnimatorController animator)
    {
        MoveSystem = moveSystem;
        Animator = animator;
    }
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
