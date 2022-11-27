public class StateManager
{
    public BasePlayerState CurrentState { get; private set; }

    public void ChangeState(BasePlayerState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    } 
}
