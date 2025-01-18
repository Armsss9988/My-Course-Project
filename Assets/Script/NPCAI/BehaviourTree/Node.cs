using UnityEngine;

[CreateAssetMenu(menuName = "AI/BehaviourTree/Node")]
public abstract class Node : ScriptableObject
{
    protected NodeState state;
    public NodeState nodeState => state;

    public abstract NodeState Evaluate(AIController controller);
}
