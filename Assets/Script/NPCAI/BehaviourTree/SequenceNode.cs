using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/BehaviourTree/Node/Sequense Node")]
public class SequenceNode : Node
{
    [SerializeField] private List<Node> children = new List<Node>();


    public override NodeState Evaluate(AIController controller)
    {
        foreach (var child in children)
        {
            var childState = child.Evaluate(controller);
            if (childState == NodeState.Failure)
            {
                state = NodeState.Failure;
                return state;
            }
        }

        state = NodeState.Success;
        return state;
    }
}
