using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ChaseToTargetState", menuName = "ChaseState/ChaseToTarget")]
public class ChaseToTarget : ChaseState
{
    private Vector3 lastKnownTargetPosition;
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);

        UpdateTargetDestination(aiController.Target.transform, aiController.GetCachedComponent<NavMeshAgent>());
    }

    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
    }

    public override void UpdateState(AIController aiController)
    {
        if (aiController.Target)
        {
            if (Vector3.Distance(lastKnownTargetPosition, aiController.Target.transform.position) > 1.0f)
            {
                UpdateTargetDestination(aiController.Target.transform, aiController.GetCachedComponent<NavMeshAgent>());
            }
        }
    }
    private void UpdateTargetDestination(Transform target, NavMeshAgent agent)
    {
        lastKnownTargetPosition = target.transform.position;
        agent.SetDestination(lastKnownTargetPosition);
    }

}
