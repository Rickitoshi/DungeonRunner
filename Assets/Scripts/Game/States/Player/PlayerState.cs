using UnityEngine;

public abstract class PlayerState
{
    protected readonly PlayerController _player;
    protected readonly PlayerAnimatorController _animator;

    protected PlayerState(PlayerController player, PlayerAnimatorController animator)
    {
        _player = player;
        _animator = animator;
    }
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
