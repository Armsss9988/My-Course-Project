using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    EnemyMovement enemyMovement;
    public GameObject hitbox;
    void Start()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    public void DisableMovement()
    {
        enemyMovement.canMove = false;
    }
    public void EnableMovement()
    {
        enemyMovement.canMove = true;
    }
    public void EnableHitBox()
    {
        if (hitbox != null)
        {
            hitbox.SetActive(true);
        }

    }
    public void DisablehitBox()
    {
        if (hitbox != null)
        {
            hitbox.SetActive(false);
        }

    }
}
