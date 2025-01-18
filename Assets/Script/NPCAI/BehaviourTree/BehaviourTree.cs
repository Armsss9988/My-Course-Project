using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/BehaviourTree/Tree")]
public class BehaviourTreeSO : ScriptableObject
{
    [SerializeField] private List<Node> nodes;

    public void Execute(AIController controller)
    {
        foreach (var node in nodes)
        {
            if (node.Evaluate(controller) == NodeState.Success)
            {
                break;
            }
        }
    }
}
