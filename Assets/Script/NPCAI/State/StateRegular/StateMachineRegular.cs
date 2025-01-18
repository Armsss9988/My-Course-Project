
public class StateMachineRegular
{
    private StateRegular currentState;
    private AIController controller;
    public StateMachineRegular(AIController aiController)
    {
        this.controller = aiController;
    }
    public void ChangeState(StateRegular newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
    public StateRegular GetState()
    {
        return currentState;
    }
}