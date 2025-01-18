public abstract class ActionNode : Node
{
    abstract protected NodeState Action(AIController controller);

    public override NodeState Evaluate(AIController controller)
    {
        state = Action(controller);
        controller.currentAcction = this;
        return state;
    }
}
