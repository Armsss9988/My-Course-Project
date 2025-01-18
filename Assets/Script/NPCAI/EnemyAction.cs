using System.Collections;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    EnemySound enemySound;
    public float timeInvincible = 1.0f;
    private void Awake()
    {
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
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        GetComponent<SpriteRenderer>().material.SetInt("_Hit", 0);
    }
    public void Dead()
    {
        StartCoroutine(DeadTime());
    }
    IEnumerator DeadTime()
    {
        enemySound.DeadSound();
        yield return new WaitForSeconds(1.4f);
        Destroy(this.gameObject);
    }

}
