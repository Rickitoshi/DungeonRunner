using UnityEngine;

public abstract class PlayerState
{
    protected readonly PlayerAnimatorController _animator;

    protected PlayerState(PlayerAnimatorController animator)
    {
        _animator = animator;
    }
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
