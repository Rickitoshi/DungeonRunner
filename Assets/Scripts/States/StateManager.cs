public class StateManager
{
    public PlayerState CurrentState { get; private set; }

    public void ChangeState(PlayerState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    } 
}
