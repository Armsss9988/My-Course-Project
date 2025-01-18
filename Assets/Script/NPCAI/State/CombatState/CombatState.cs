using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "ChaseState")]
public class CombatState : MoveState
{
    [SerializeField]
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
        if (!aiController.IsMoving)
        {
            SetMovingTarget(aiController, GetRandomPositionInAttackZone(aiController));
        }
        base.FixedUpdateState(aiController);
    }

    public override void UpdateState(AIController aiController)
    {
        base.UpdateState(aiController);
    }

    protected Vector2 GetRandomPositionInAttackZone(AIController aiController)
    {

        Vector2 targetPos = aiController.Target.transform.position;
        NPCData enemy = aiController.GetCachedComponent<NPCData>();
        float attackZoneRadius = (enemy.maxAttackZone + enemy.minAttackZone) / 2f;
        float minDistanceFromPlayer = enemy.minAttackZone;
        Rigidbody2D rb2d = aiController.GetComponent<Rigidbody2D>();

        for (int i = 0; i < 10; i++)
        {
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);
            Vector2 playerDir = targetPos - (Vector2)rb2d.position;
            float angleOffset = Mathf.Sign(Vector2.Dot(playerDir, Vector2.up)) * Mathf.PI;
            float adjustedAngle = randomAngle + angleOffset;
            Vector2 randomOffset = new Vector2(Mathf.Cos(adjustedAngle), Mathf.Sin(adjustedAngle)) * attackZoneRadius;
            Vector2 target = randomOffset + targetPos;
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (target - rb2d.position).normalized * enemy.targetColliderOffset, target - rb2d.position, Vector2.Distance(rb2d.position, target));

            if (hit.collider == null)
            {
                if (Vector2.Distance(target, targetPos) >= minDistanceFromPlayer)
                {
                    return target;
                }
            }
        }
        return targetPos + Random.insideUnitCircle.normalized * (attackZoneRadius / 4f);
    }


}
