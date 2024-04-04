using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    EnemyRangeDetect enemyRangeDetect;
    Vector2 firstPos;
    bool isMoving = false;
    public bool canMove = true;
    Vector2 target;
    public bool isRandomMove = true;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRangeDetect = GetComponent<EnemyRangeDetect>();
        firstPos = GetComponent<Rigidbody2D>().position;
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0f);
            isMoving = false;
        }
        if (rb2d.velocity.magnitude <= 0.05f)
        {
            animator.SetFloat("Speed", 0f);
        }

        if (canMove)
        {
            EnemyMove();
        }
    }

    void EnemyMove()
    {


        if (!enemyRangeDetect.IsInDetectRange())
        {
            if (isRandomMove)
            {
                if (!isMoving)
                {
                    StartCoroutine(RandomRunningTarget(new Vector2(Random.Range(firstPos.x - 3, firstPos.x + 3), Random.Range(firstPos.y - 3, firstPos.y + 3))));
                }


                animator.SetFloat("Speed", (target - rb2d.position).magnitude);

                rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, target, Time.deltaTime * enemy.speed));
                CheckFlipx(target);
            }
        }

        else if (enemyRangeDetect.IsInDetectRange() && enemy.target != null)
        {
            Vector2 targetPos = enemy.target.GetComponent<Rigidbody2D>().position;
            float distanceToTarget = Vector2.Distance(transform.position, targetPos);
            if (distanceToTarget > enemy.maxAttackZone)
            {

                if (!isMoving)
                {
                    RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (targetPos - rb2d.position).normalized * enemy.targetColliderOffset, targetPos - rb2d.position, enemy.checkingRange);
                    Debug.DrawRay(rb2d.position + (targetPos - rb2d.position).normalized * enemy.targetColliderOffset, targetPos - rb2d.position, Color.red, enemy.checkingRange);
                    if (hit.collider != null && hit.collider.gameObject != enemy.target)
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            Debug.DrawRay(rb2d.position, targetPos - rb2d.position, Color.red, Vector2.Distance(targetPos, rb2d.position));
                        }
                        Debug.Log("target hit: " + hit.collider.gameObject.name);
                        Vector2 contactPoint = hit.point;
                        Vector2 newDirection = Vector2.Perpendicular(hit.normal).normalized;
                        newDirection += new Vector2(Random.value * 0.1f, Random.value * 0.1f);
                        target = contactPoint + newDirection * (targetPos - rb2d.position).magnitude;

                    }
                    else
                    {
                        target = targetPos;
                    }
                    StartCoroutine(RandomRunningTarget(target));
                }
                rb2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * enemy.speed * 1.5f));
                animator.SetFloat("Speed", (targetPos - rb2d.position).magnitude);
                CheckFlipx(targetPos);
            }
            else if (distanceToTarget <= enemy.minAttackZone)
            {
                if (!isMoving)
                {
                    StartCoroutine(RandomRunningTarget(GetRandomEscapePosition(targetPos, enemy.maxAttackZone)));
                }

                rb2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * enemy.speed * 1.5f));

                animator.SetFloat("Speed", (target - (Vector2)transform.position).magnitude);

                CheckFlipx(target);
            }
            else
            {
                if (!isMoving)
                {
                    StartCoroutine(RandomRunningTarget(GetRandomPositionInAttackZone()));
                }

                rb2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * enemy.speed));
                animator.SetFloat("Speed", (target - (Vector2)transform.position).magnitude);
                CheckFlipx(target);
            }
        }
    }

    IEnumerator RandomRunningTarget(Vector2 newTarget)
    {
        isMoving = true;
        target = newTarget;
        yield return new WaitForSeconds(3);
        isMoving = false;
    }

    Vector2 GetRandomEscapePosition(Vector2 playerPos, float escapeRadius)
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 escapePos = playerPos - randomDir * escapeRadius;

        // Check for obstacles on the path
        RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (escapePos - rb2d.position).normalized * enemy.targetColliderOffset, escapePos - rb2d.position, Vector2.Distance(rb2d.position, escapePos));
        if (escapeRadius < 0.05f)
        {
            return enemy.transform.position;
        }
        if (hit.collider == null) // No obstacle
        {
            return escapePos;
        }
        else
        {

            return GetRandomEscapePosition(playerPos, escapeRadius * 0.9f);
        }
    }

    Vector2 GetRandomPositionInAttackZone()
    {
        Vector2 targetPos = enemy.target.transform.position;
        float attackZoneRadius = (enemy.maxAttackZone + enemy.minAttackZone) / 2f;
        float minDistanceFromPlayer = enemy.minAttackZone;

        for (int i = 0; i < 10; i++)
        {
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);
            Vector2 playerDir = targetPos - (Vector2)transform.position;
            float angleOffset = Mathf.Sign(Vector2.Dot(playerDir, Vector2.up)) * Mathf.PI;
            float adjustedAngle = randomAngle + angleOffset;

            Vector2 randomOffset = new Vector2(Mathf.Cos(adjustedAngle), Mathf.Sin(adjustedAngle)) * attackZoneRadius;
            Vector2 target = randomOffset + targetPos;
            // Check for obstacles on the path
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + (target - rb2d.position).normalized * enemy.targetColliderOffset, target - rb2d.position, Vector2.Distance(rb2d.position, target));

            if (hit.collider == null)
            {
                if (Vector2.Distance(target, targetPos) >= minDistanceFromPlayer)
                {
                    return target;
                }
            }
        }

        // No suitable position found, try a random point within a smaller radius near the target
        return targetPos + Random.insideUnitCircle.normalized * (attackZoneRadius / 4f);
    }

    public void CheckFlipx(Vector3 target)
    {
        if ((target.x - GetComponent<Rigidbody2D>().position.x) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}