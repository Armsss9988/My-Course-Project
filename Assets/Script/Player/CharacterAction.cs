using System.Collections;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{

    public float dropPos;
    Character character;
    CharacterSelectedItem playerSelectedItem;
    CharacterMovement characterMovement;
    CharacterAnimation characterAnimation;
    CharacterSound playerSound;
    GameObject hand;
    GameObject hitbox;
    Item projectile;
    bool isAbleToAttack = true;
    void Start()
    {
        character = GetComponent<Character>();
        playerSound = GetComponent<CharacterSound>();
        playerSelectedItem = GetComponent<CharacterSelectedItem>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();
        hand = character.transform.Find("Hand").gameObject;
        hitbox = character.transform.Find("Hitbox").gameObject;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (playerSelectedItem.GetSelectedItem() != null && (playerSelectedItem.GetSelectedItem().data is WeaponData))
            {
                Attack();
            }
        }
    }


    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = new(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.gameObject.layer = this.gameObject.layer;
        droppedItem.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponent<SpriteRenderer>().sortingLayerName;
        StartCoroutine(DropTime(droppedItem));
        droppedItem.rb2d.AddForce(spawnOffset * dropPos, ForceMode2D.Impulse);
    }
    IEnumerator DropTime(Item item)

    {
        item.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        item.GetComponent<Collider2D>().enabled = true;
    }
    void Attack()
    {
        if (playerSelectedItem.GetSelectedItem() != null && isAbleToAttack)
        {

            playerSelectedItem.GetSelectedItem().data.Use(character);
            if (playerSelectedItem.GetSelectedItem().data is WeaponData weaponData)
            {
                float attackSpeed = weaponData.attackSpeed * character.maxAttackSpeed;
                isAbleToAttack = false;
                if (weaponData is MeleeWeaponData melee)
                {
                    CharacterAnimation characterAnimation = character.GetComponent<CharacterAnimation>();
                    Vector3 lookDirection = characterAnimation.LookDirection() - hand.transform.position;
                    lookDirection.Normalize();
                    hitbox.GetComponent<HitCollider>().SetWeaponData(melee);
                    hitbox.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
                }
                if (weaponData is RangeWeaponData range)
                {
                    if (character.IsArrowInInventory())
                    {
                        Vector3 direction = (characterAnimation.LookDirection() - hitbox.transform.position).normalized;
                        ShootArrow(hitbox, direction, range);
                        character.RemoveArrow(1);
                    }

                }
                StartCoroutine(AttackTime(1 / attackSpeed));
            }

        }
    }
    public void SetArrow(Item arrow)
    {
        projectile = arrow;
    }
    IEnumerator AttackTime(float attackSpeed)
    {


        yield return new WaitForSeconds(attackSpeed);
        isAbleToAttack = true;
    }
    public void BeingHit()
    {
        StartCoroutine(BeinghitTime());
    }
    IEnumerator BeinghitTime()
    {
        characterMovement.canMove = false;
        playerSound.GetHitSound();
        HitSprite();
        GetComponent<SpriteRenderer>().material.SetInt("_Hit", 1);
        characterAnimation.ToogleAnimator(false);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        characterMovement.canMove = true;
        StopHitSprite();
        characterAnimation.ToogleAnimator(true);

    }
    public void HitSprite()
    {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.material.SetInt("_Hit", 1);
        }
    }
    public void StopHitSprite()
    {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.material.SetInt("_Hit", 0);
        }
    }
    public void ShootArrow(GameObject user, Vector3 direction, RangeWeaponData rangeWeaponData)
    {
        StartCoroutine(ChaseTime(user, direction, rangeWeaponData));


    }
    IEnumerator ChaseTime(GameObject user, Vector3 direction, RangeWeaponData rangeWeaponData)
    {
        yield return new WaitForSeconds(0.5f);
        Item project = Instantiate(projectile, user.transform.position + direction + (Vector3.up * 0.1f), Quaternion.identity);
        project.GetComponent<Arrow>().SetSource(this.gameObject);
        project.gameObject.layer = user.layer;
        project.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = user.GetComponent<SpriteRenderer>().sortingLayerName;
        project.gameObject.GetComponent<SpriteRenderer>().sortingOrder = user.GetComponent<SpriteRenderer>().sortingOrder;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        project.GetComponent<Rigidbody2D>().rotation = angle;
        project.GetComponent<Arrow>().SetDirection(user.transform.position, characterAnimation.LookDirection());
        project.GetComponent<Arrow>().isEnable = true;
    }

}
