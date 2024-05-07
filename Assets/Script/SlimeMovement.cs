using System.Collections;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2d;
    public Animator animator;
    bool isMoving = false;
    SpriteRenderer spriteRenderer;
    Vector2 target;
    Vector2 firstPos;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        firstPos = GetComponent<Rigidbody2D>().position;
    }

    void Update()
    {
        RandomMove();
    }
    private void FixedUpdate()
    {
    }
    private void RandomMove()
    {
        if (!isMoving)
        {
            StartCoroutine(RandomRunningTarget());
        }
        animator.SetFloat("LookX", target.x - GetComponent<Rigidbody2D>().position.x);
        animator.SetFloat("Speed", Mathf.Abs(target.x - GetComponent<Rigidbody2D>().position.x));
        CheckFlipx(target);
        GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(GetComponent<Rigidbody2D>().position, target, Time.deltaTime * speed));
    }
    IEnumerator RandomRunningTarget()
    {
        isMoving = true;
        target = new Vector2(Random.Range(firstPos.x - 3, firstPos.x + 3), Random.Range(firstPos.y - 3, firstPos.y + 3));
        yield return new WaitForSeconds(3);
        isMoving = false;
    }
    void CheckFlipx(Vector3 target)
    {
        if ((target.x - GetComponent<Rigidbody2D>().position.x) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
