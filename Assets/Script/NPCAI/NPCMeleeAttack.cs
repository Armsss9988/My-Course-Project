using System.Collections;
using UnityEngine;

public class NPCMeleeAttack : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AIController controller;
    public GameObject hitbox;
    Animator animator;
    public bool isAbleToAttack = true;
    Vector2 direction;
    void Awake()
    {

        controller = GetComponent<AIController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void Execute()
    {
        if (controller.Target != null && isAbleToAttack)
        {
            CheckAttackFlipx();
            hitbox.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Attack();

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
        yield return new WaitForSeconds(controller.npcData.attackSpeed);
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
