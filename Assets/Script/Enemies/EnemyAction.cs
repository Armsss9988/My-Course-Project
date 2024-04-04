using System.Collections;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    EnemyAnimation enemyAnimation;
    EnemyMovement enemyMovement;
    EnemySound enemySound;
    public float timeInvincible = 1.0f;
    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemySound = GetComponent<EnemySound>();
    }
    public void BeingHit()
    {
        StartCoroutine(BeinghitTime());
    }
    IEnumerator BeinghitTime()
    {
        enemySound.GetHitSound();
        GetComponent<SpriteRenderer>().material.SetInt("_Hit", 1);
        enemyMovement.canMove = false;
        enemyAnimation.ToogleAnimator(false);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        enemyMovement.canMove = true;
        GetComponent<SpriteRenderer>().material.SetInt("_Hit", 0);
        enemyAnimation.ToogleAnimator(true);

    }
    public void Dead()
    {
        StartCoroutine(DeadTime());
    }
    IEnumerator DeadTime()
    {
        enemyAnimation.DeadAnimation();
        enemySound.DeadSound();
        yield return new WaitForSeconds(1.4f);
        Destroy(this.gameObject);
    }

}
