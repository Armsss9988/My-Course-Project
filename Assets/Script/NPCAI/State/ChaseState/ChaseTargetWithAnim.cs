using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ChaseToTargetWithAnim", menuName = "ChaseState/ChaseToTargetWithAnim")]
public class ChaseTargetWithAnim : ChaseToTarget
{
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
        aiController.animator.SetBool("isRunning", true);
    }

    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
        aiController.animator.SetBool("isRunning", false);
    }
}
