using UnityEngine;

public abstract class PlayerState
{
    protected readonly PlayerController _player;
    protected readonly Animator _animator;

    protected PlayerState(PlayerController player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
