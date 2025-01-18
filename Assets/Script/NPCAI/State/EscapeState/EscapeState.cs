using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "ChaseState")]
public class EscapeState : MoveState
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

        if (!aiController.IsMoving)
        {
            float escapeRadius = aiController.GetCachedComponent<NPCData>().maxAttackZone;
            SetMovingTarget(aiController, GetRandomEscapePosition(aiController, escapeRadius));
        }
        base.FixedUpdateState(aiController);
    }

    public override void UpdateState(AIController aiController)
    {
        base.UpdateState(aiController);
    }
    protected Vector2 GetRandomEscapePosition(AIController aiController, float escapeRadius)
    {
        Vector2 targetPos = aiController.Target.transform.position;
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 escapePos = targetPos - randomDir * escapeRadius;
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        NPCData enemy = aiController.GetCachedComponent<NPCData>();
        RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (escapePos - rb2d.position).normalized * enemy.targetColliderOffset, escapePos - rb2d.position, Vector2.Distance(rb2d.position, escapePos));
        if (escapeRadius < 0.05f)
        {
            return enemy.transform.position;
        }
        if (hit.collider == null)
        {
            return escapePos;
        }
        else
        {

            return GetRandomEscapePosition(aiController, escapeRadius * 0.9f);
        }
    }

}
