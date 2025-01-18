
using System.Collections;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator animator;
    GameObject source;
    ArrowData arrowData;
    float sourceDamge = 0f;
    public bool isEnable = false;
    public float forceMagnitude = 5f;
    public float torqueMagnitude = 5f;
    Vector2 direction, fistPos;
    public float distance;
    Collectable collectable;
    public AudioClip release, hit;
    AudioSource audioSource;

    void Awake()
    {
        collectable = GetComponent<Collectable>();
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        arrowData = (ArrowData)GetComponent<Item>().data;
        fistPos = transform.position;
        StartCoroutine(DelayedTriggerCoroutine());
        audioSource.PlayOneShot(release);
    }
    private void Update()
    {
        if (isEnable)
        {
            SetForce();

        }

        if (((Vector2)this.transform.position - fistPos).magnitude >= distance)
        {
            rb2D.angularVelocity = 0f;
            this.rb2D.linearVelocity = Vector2.zero;
            isEnable = false;
        }
        if (this.rb2D.linearVelocity == Vector2.zero)
        {
            animator.SetTrigger("Disable");
            collectable.isCollectable = true;
        };
    }
    public void SetDirection(Vector2 current, Vector2 target)
    {
        this.direction = (target - current).normalized;
    }

    public void SetForce()
    {
        if (forceMagnitude > 0 && torqueMagnitude > 0)
        {
            float angle = transform.eulerAngles.z;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            Vector3 torqueVector = Vector3.Cross(direction, Vector3.forward) * torqueMagnitude;
            GetComponent<Rigidbody2D>().AddForce(dir * forceMagnitude, ForceMode2D.Impulse);

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
    public void SetSource(GameObject gameObject)
    {
        this.source = gameObject;
        if (source != null)
        {
            if ((source.TryGetComponent<Character>(out var player)))
            {
                sourceDamge = player.damage;
            }
            if (source.TryGetComponent<NPCData>(out var enemy))
            {
                sourceDamge = enemy.damage;
            }
        }
    }

    IEnumerator DelayedUpdate()
    {
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator DelayedTriggerCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnable)
        {
            if (collision != null && source)
            {
                if (this.gameObject.IsTargetThisObject(collision.gameObject) && (collision.gameObject.GetComponent<Collider2D>() != source.GetComponent<Collider2D>()))
                {

                    if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
                    {
                        target.SourceAttackSound(hit);
                        target.Force((collision.transform.position - this.transform.position).normalized, 80);
                        target.ChangeHealth(-(arrowData.damage + sourceDamge));
                        Destroy(this.gameObject);
                    }
                    if (collision.gameObject.TryGetComponent<IAttackable>(out var targetAttackable))
                    {
                        if (source != null)
                        {
                            targetAttackable.SetTarget(source);
                        }

                    }
                    if (source.TryGetComponent<Character>(out var character))
                    {
                        if (collision.gameObject.TryGetComponent<NPCData>(out var enemy))
                        {
                            if (enemy.currentHealth <= 0f)
                            {
                                InteractionManager.instance.Enemykilled(enemy.GetComponent<Actor>().ActorName);
                            }
                        }
                    }
                }
            }
        }
    }
}

