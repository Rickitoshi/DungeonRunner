using UnityEngine;

public class IdleState: IState
{
    private readonly PlayerController _player;
    private readonly Animator _animator;

    public IdleState(PlayerController player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }
    
    public void Enter()
    {
        _animator.SetTrigger(PlayerController.IDLE);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
}
