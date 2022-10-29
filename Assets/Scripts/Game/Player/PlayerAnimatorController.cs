using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private const string RUN = "Run";
    private const string IDLE = "Idle";
    private const string DIE = "Die";
    
    private readonly int Run = Animator.StringToHash(RUN);
    private readonly int Idle = Animator.StringToHash(IDLE);
    private readonly int Die = Animator.StringToHash(DIE);
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }

    public void SetRun()
    {
        _animator.SetTrigger(Run);
    }

    public void SetDie()
    {
        _animator.SetTrigger(Die);
    }
}
