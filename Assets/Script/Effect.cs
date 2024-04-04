using UnityEngine;

public class Effect : MonoBehaviour
{
    GameObject source;
    float damage;

    public void SetSource(GameObject gameObject)
    {
        source = gameObject;
        SetDamage();
    }
    void SetDamage()
    {
        damage = source.GetComponent<Enemy>().damage;
    }

    public void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.IsTargetThisObject(collision.gameObject))
        {
            if (collision.TryGetComponent<IDamageable>(out var target))
            {
                target.Force((collision.transform.position - this.transform.position).normalized, 80);
                target.ChangeHealth(-(damage));
            };
            if (collision.TryGetComponent<IAttackable>(out var targetAttackable))
            {
                if (source != null)
                {
                    targetAttackable.SetTarget(source);
                }

            }
        }
    }
}
