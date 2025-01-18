using UnityEngine;
// ReSharper disable All

public class EnemyRangeAttack : MonoBehaviour
{
    Detector enemyRangeDetect;
    SpriteRenderer spriteRenderer;
    EnemySound enemySound;
    NPCData enemy;
    private bool isThrowing = false;
    public bool isMultipleDirection = false;

    void Awake()
    {
        enemyRangeDetect = GetComponent<Detector>();
        enemySound = GetComponent<EnemySound>();
        enemy = GetComponent<NPCData>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrowBullet();
    }
    public void ThrowBullet()
    {
        /* if (enemyRangeDetect.IsInAttackRange())
         {
             if (!isThrowing && enemy.target)
             {
                 StartCoroutine(TimeThrow());
             }
         }*/
    }
    /*public IEnumerator TimeThrow()
    {

        isThrowing = true;
        Vector3 targetPos = enemy.target.transform.position;
        Vector2 direction = (targetPos - transform.position).normalized;


        CheckAttackFlipx(targetPos);
        enemySound.AttackSound();
        yield return new WaitForSeconds(0.2f);

        GameObject bull = Instantiate(enemy.bullet, transform.position + (targetPos - transform.position).normalized * 0.3f, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bull.GetComponent<Rigidbody2D>().rotation = angle;

        if (bull.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.SetSource(this.gameObject);
            bullet.SetDirection(this.transform.position, targetPos);
        }
        if (bull.TryGetComponent<Arrow>(out var arrow))
        {
            arrow.isEnable = true;
            arrow.SetSource(this.gameObject);
            arrow.SetDirection(this.transform.position, targetPos);
        }

        bull.layer = this.gameObject.layer;
        bull.GetComponent<SpriteRenderer>().sortingLayerName = this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
        bull.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

        yield return new WaitForSeconds(enemy.attackSpeed);
        isThrowing = false;
    }
    public void CheckAttackFlipx(Vector3 target)
    {
        if ((target.x - GetComponent<Rigidbody2D>().position.x) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }*/
}
