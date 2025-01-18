using UnityEngine;
public class AIMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool isFourDirection;
    private AIController aiController;
    private Vector2 prevPosition;
    public void Awake()
    {
        aiController = GetComponent<AIController>();
    }
    public void Update()
    {


        Vector2 targetPosition = aiController.MovingTarget;
        Animator animator = aiController.GetCachedComponent<Animator>();
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        Vector2 direction = targetPosition - rb2d.position;
        if (isFourDirection)
        {


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
    public void FixedUpdate()
    {
        Vector2 targetPosition = aiController.MovingTarget;
        NPCData enemy = aiController.GetCachedComponent<NPCData>();
        Animator animator = aiController.GetCachedComponent<Animator>();
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (targetPosition - rb2d.position).normalized * enemy.targetColliderOffset, targetPosition - rb2d.position, aiController.Speed * Time.fixedDeltaTime);
        Debug.DrawRay(rb2d.position + (targetPosition - rb2d.position).normalized * enemy.targetColliderOffset, (targetPosition - rb2d.position).normalized * aiController.Speed * Time.fixedDeltaTime);
        if (hit.collider != null)
        {
            rb2d.linearVelocity = Vector2.zero;
            Debug.Log("Obstacle detected: " + hit.collider.name);
        }
        else
        {
            rb2d.linearVelocity = (targetPosition - rb2d.position).normalized * aiController.Speed;
        }
        Debug.Log(rb2d.linearVelocity + " and " + (float)rb2d.linearVelocity.magnitude);
        animator.SetFloat("Speed", (float)rb2d.linearVelocity.magnitude);

    }

    public void CheckFlipx(AIController aiController)
    {
        float targetX = aiController.MovingTarget.x;
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = aiController.GetCachedComponent<SpriteRenderer>();
        if ((targetX - rb2d.position.x) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
