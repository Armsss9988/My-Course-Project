public interface IAIState
{
    public void EnterState(AIController aiController);
    public void UpdateState(AIController aiController);
    public void FixedUpdateState(AIController aiController);
    public void ExitState(AIController aiController);
}
public interface IAIStateRegular
{
    public void EnterState();
    public void UpdateState();
    public void ExitState();
}
