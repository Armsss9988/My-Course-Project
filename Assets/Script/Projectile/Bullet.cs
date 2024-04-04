using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class Bullet : MonoBehaviour
{
    float curTime;
    GameObject source;
    public float activeTime = 3f;
    public GameObject explode;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    public float forceMagnitude = 5f;
    public float torqueMagnitude = 5f;
    Vector2 direction, fistPos;
    public float distance;
    public List<Tag> attackTags;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        curTime = activeTime;
        fistPos = this.transform.position;

    }
    private void Update()
    {
        SetForce();
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            DesTroyBullet();
        }
        if (((Vector2)this.transform.position - fistPos).magnitude >= distance)
        {
            rb2D.angularVelocity = 0f;
            this.rb2D.velocity = Vector2.zero;
        }
    }
    public void SetSource(GameObject gameObject)
    {
        if (gameObject != null)
        {
            source = gameObject;
        }
    }
    public void DesTroyBullet()
    {
        GameObject effect = Instantiate(explode, this.transform.position, Quaternion.identity);
        if (source != null)
        {
            effect.GetComponent<Effect>().SetSource(source);
        }
        effect.layer = this.gameObject.layer;
        effect.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = this.spriteRenderer.sortingLayerName;
        effect.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.spriteRenderer.sortingOrder;
        Destroy(this.gameObject);
    }
    public void SetDirection(Vector2 current, Vector2 target)
    {
        this.direction = (target - current).normalized;
    }

    public void SetForce()
    {
        if (forceMagnitude > 0 && torqueMagnitude > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
            Vector3 torqueVector = Vector3.Cross(direction, Vector3.forward) * torqueMagnitude;
            if (direction.x < 0)
            {
                GetComponent<Rigidbody2D>().AddTorque(torqueVector.magnitude);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddTorque(-torqueVector.magnitude);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (this.gameObject.IsTargetThisObject(collision.gameObject))
            {
                DesTroyBullet();
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


}
