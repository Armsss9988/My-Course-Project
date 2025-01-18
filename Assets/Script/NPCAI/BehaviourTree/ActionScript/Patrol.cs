using UnityEngine;
[CreateAssetMenu(menuName = "AI/BehaviourTree/ActionNode/Patrol")]
public class Patrol : ActionNode
{
    [SerializeField] private float patrolRange = 3f;
    [SerializeField] private float stopDistance = 0.1f;
    protected override NodeState Action(AIController controller)
    {
        Rigidbody2D rb2d = controller.GetCachedComponent<Rigidbody2D>();
        if ((rb2d.position - controller.MovingTarget).magnitude <= 0.1f || rb2d.linearVelocity.magnitude == 0)
        {
            Vector2 firstPosition = controller.FirstPosition;
            Vector2 newTarget = new Vector2(
                Random.Range(firstPosition.x - patrolRange, firstPosition.x + patrolRange),
                Random.Range(firstPosition.y - patrolRange, firstPosition.y + patrolRange)
            );

            Debug.Log("New Patrol Target: " + newTarget);
            controller.MovingTarget = newTarget;
        };
        return NodeState.Running;
    }
}