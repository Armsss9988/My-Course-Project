using UnityEngine;
[CreateAssetMenu(fileName = "MoveState", menuName = "MoveState")]
public class MoveState : AIState
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool isFourDirection;
    public override void EnterState(AIController aiController)
    {
        aiController.IsMoving = true;
    }
    public override void ExitState(AIController aiController)
    {
        aiController.IsMoving = false;


    }

    public override void UpdateState(AIController aiController)
    {


        Vector2 targetPosition = aiController.MovingTarget;
        Animator animator = aiController.GetCachedComponent<Animator>();
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        if (isFourDirection)
        {
            Vector2 direction = targetPosition - rb2d.position;

            Vector2 lookDirection = new();
            if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
            {
                lookDirection.Set(direction.x, direction.y);
                lookDirection.Normalize();
                animator.SetFloat("Look X", lookDirection.x);
                animator.SetFloat("Look Y", lookDirection.y);
                animator.SetFloat("Attack Speed", 1f);
                animator.SetFloat("Movement Speed", 1f);
            }
        }
        else
        {
            CheckFlipx(aiController);
        }

    }
    public override void FixedUpdateState(AIController aiController)
    {
        Vector2 targetPosition = aiController.MovingTarget;
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        Animator animator = aiController.GetCachedComponent<Animator>();
        animator.SetFloat("Speed", (targetPosition - rb2d.position).normalized.magnitude);
        rb2d.linearVelocity = (targetPosition - rb2d.position).normalized * aiController.Speed;
    }
    protected void SetMovingTarget(AIController aiController, Vector2 newTarget)
    {
        aiController.IsMoving = true;
        aiController.MovingTarget = newTarget;
    }
}
