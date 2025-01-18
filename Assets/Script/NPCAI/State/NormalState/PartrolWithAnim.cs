using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "PartrolWithAnim", menuName = "NormalState/PartrolWithAnim")]
public class PatrolWithAnim : NormalState
{
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
        aiController.animator.SetBool("isWalking", true);

        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.updatePosition = false;

    }
    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
        aiController.animator.SetBool("isWalking", false);
    }

    public void OnAnimatorMoveState(AIController aiController)
    {
        Animator animator = aiController.animator;
        Vector3 position = animator.rootPosition;
        aiController.transform.position = position;
    }

}
