using UnityEngine;

public class HitCollider : MonoBehaviour
{
    MeleeWeaponData meleeWeaponData;
    GameObject atttacker;
    void Awake()
    {
        atttacker = this.transform.parent.gameObject;
    }
    public void SetWeaponData(MeleeWeaponData weaponData)
    {
        if (weaponData != null)
        {
            meleeWeaponData = weaponData;
            Debug.Log("Weapon: " + meleeWeaponData.itemName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (this.transform.parent.gameObject.IsTargetThisObject(collision.gameObject) && collision.gameObject != atttacker)
        {
            if (collision.TryGetComponent<IDamageable>(out var target))
            {
                target.Force(collision.transform.position - this.transform.position, 50);
                target.ChangeHealth(-meleeWeaponData.damage);
            }
            if (collision.TryGetComponent<IAttackable>(out var targetAtackable))
            {
                targetAtackable.SetTarget(atttacker);
            }
            if (collision.TryGetComponent<Enemy>(out var enemy))
            {
                if (enemy.currentHealth <= 0f)
                {
                    InteractionManager.instance.Enemykilled(enemy.GetComponent<Actor>().ActorName);
                }
            }
        }

    }

}
