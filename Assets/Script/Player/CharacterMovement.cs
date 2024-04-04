using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2d;
    public bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    public void MovementPosition(float horizontal, float vertical)
    {
        if (canMove)
        {
            Vector2 position = rigidbody2d.position;
            position.x += speed * horizontal * Time.deltaTime;
            position.y += speed * vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

    }


}
