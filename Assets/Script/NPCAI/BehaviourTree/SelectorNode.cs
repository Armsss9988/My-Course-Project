using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/BehaviourTree/Node/Selector Node")]
public class SelectorNode : Node
{
    [SerializeField] private List<Node> children = new List<Node>();


    public override NodeState Evaluate(AIController controller)
    {
        foreach (var child in children)
        {
            var childState = child.Evaluate(controller);
            if (childState == NodeState.Success)
            {
                state = NodeState.Success;
                return state;
            }
        }

        state = NodeState.Failure;
        return state;
    }
}
