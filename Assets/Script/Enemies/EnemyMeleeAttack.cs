using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    EnemyAnimation enemyAnimation;
    EnemyMovement enemyMovement;
    SpriteRenderer spriteRenderer;
    EnemySound enemySound;
    Enemy enemy;
    public GameObject hitbox;
    Animator animator;
    public bool isAbleToAttack = true;
    Vector2 direction;
    // Start is called before the first frame update
    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemySound = GetComponent<EnemySound>();
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.target != null)
        {
            direction = (enemy.target.transform.position - hitbox.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)enemy.transform.position, direction, 1f);
            Debug.DrawRay((Vector2)enemy.transform.position, direction, Color.red, 0.2f);
            if (!enemyMovement.canMove)
            {
                CheckAttackFlipx();
                hitbox.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
            if (hit.collider != null)
            {
                if (GetComponent<EnemyRangeDetect>().IsInAttackRange() || (hit.collider.gameObject == enemy.target))
                {
                    if (isAbleToAttack)
                    {
                        Attack();
                    }
                }
            }

        }
    }
    public void Attack()
    {
        animator.SetFloat("Fight X", direction.x);
        animator.SetFloat("Fight Y", direction.y);
        StartCoroutine(AttackTime());
    }
    public IEnumerator AttackTime()
    {
        isAbleToAttack = false;
        enemyAnimation.AttackAnimation();
        enemySound.AttackSound();
        enemySound.AttackWeaponSound();
        yield return new WaitForSeconds(enemy.attackSpeed);
        isAbleToAttack = true;
    }
    public void CheckAttackFlipx()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
