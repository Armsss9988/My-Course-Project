using UnityEngine;
[CreateAssetMenu(menuName = "AI/BehaviourTree/Node/Condition Node")]

public class ConditionNode : Node
{
    [SerializeField] Condition condition;
    public override NodeState Evaluate(AIController controller)
    {
        if (controller.conditions[condition])
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}
