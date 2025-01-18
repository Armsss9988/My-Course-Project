using UnityEngine;
[CreateAssetMenu(menuName = "AI/BehaviourTree/ActionNode/MelleAttack")]
public class MelleAttack : ActionNode
{
    protected override NodeState Action(AIController controller)
    {
        if (controller.Target != null)
        {

            if (controller.TryGetComponent<NPCMeleeAttack>(out var npcMelee))
            {
                npcMelee.Execute();
            }
            else
            {
                Debug.Log("Need Attack Script");
            }

        }
        return NodeState.Running;
    }
}