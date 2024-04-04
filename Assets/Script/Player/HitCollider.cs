using UnityEngine;

public class HitCollider : MonoBehaviour
{
    MeleeWeaponData meleeWeaponData;
    CharacterSound playerSound;
    GameObject player;
    void Awake()
    {
        playerSound = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSound>();
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

        if (this.transform.parent.gameObject.IsTargetThisObject(collision.gameObject))
        {
            if (collision.TryGetComponent<IDamageable>(out var target))
            {
                playerSound.AttackHitSound();
                target.Force(collision.transform.position - this.transform.position, 50);
                target.ChangeHealth(-meleeWeaponData.damage);
            }
            if (collision.TryGetComponent<IAttackable>(out var targetAtackable))
            {
                targetAtackable.SetTarget(player.gameObject);
            }
        }

    }

}
