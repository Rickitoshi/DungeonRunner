using UnityEngine;

public class IdleState: IState
{
    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }

    public void Exit()
    {
        Debug.Log("Exit IdleState");
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
}
