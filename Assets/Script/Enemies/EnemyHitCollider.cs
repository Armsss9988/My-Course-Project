using UnityEngine;

public class EnemyHitCollider : MonoBehaviour
{
    Enemy enemy;
    EnemySound enemySound;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemySound = GetComponentInParent<EnemySound>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (enemy.gameObject.IsTargetThisObject(collision.gameObject))
        {
            if (collision.TryGetComponent<IDamageable>(out var target))
            {
                enemySound.AttackWeaponHitSound();
                target.Force((collision.transform.position - this.transform.position).normalized, 80);
                target.ChangeHealth(-(enemy.damage));
            }
            if (collision.TryGetComponent<IAttackable>(out var targetAttackable))
            {
                targetAttackable.SetTarget(enemy.gameObject);
            }
        }
    }
}
