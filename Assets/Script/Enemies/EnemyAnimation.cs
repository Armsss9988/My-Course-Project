using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DeadAnimation()
    {
        animator.SetTrigger("Dead");
    }
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public void ToogleAnimator(bool bol)
    {

        animator.enabled = bol;

    }
    public void SetDirectionAttack(Vector2 direction)
    {
        animator.SetFloat("Fight X", direction.x);
        animator.SetFloat("Fight Y", direction.y);
    }
}
