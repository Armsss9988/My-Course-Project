using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "ChaseState")]
public class ChaseState : MoveState
{

    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
    }

    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
    }

    public override void FixedUpdateState(AIController aiController)
    {


        Vector2 targetPos = aiController.Target.transform.position;
        NPCData enemy = aiController.GetCachedComponent<NPCData>();
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        float distanceToTarget = Vector2.Distance(rb2d.position, targetPos);
        Vector2 movingTarget;
        if (distanceToTarget > enemy.maxAttackZone)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (targetPos - rb2d.position).normalized * enemy.targetColliderOffset, targetPos - rb2d.position, enemy.checkingRange);
            Debug.DrawRay(rb2d.position + (targetPos - rb2d.position).normalized * enemy.targetColliderOffset, targetPos - rb2d.position, Color.red, enemy.checkingRange);
            if (hit.collider != null && hit.collider.gameObject != aiController.Target)
            {
                if (hit.collider == this)
                {
                    Debug.DrawRay(rb2d.position, targetPos - rb2d.position, Color.red, Vector2.Distance(targetPos, rb2d.position));
                }
                Vector2 contactPoint = hit.point;
                Vector2 newDirection = Vector2.Perpendicular(hit.normal).normalized;
                newDirection += new Vector2(Random.value * 0.1f, Random.value * 0.1f);
                movingTarget = contactPoint + newDirection * (targetPos - rb2d.position).magnitude;
            }
            else
            {
                movingTarget = targetPos;
            }
            if (!aiController.IsMoving)
            {
                SetMovingTarget(aiController, movingTarget);
            }
        }
        else if (distanceToTarget <= enemy.minAttackZone)
        {

        }
        else
        {

        }
        base.FixedUpdateState(aiController);
    }

    public override void UpdateState(AIController aiController)
    {
        base.UpdateState(aiController);
    }

}
