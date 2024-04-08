using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Character character;
    public bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame

    public void MovementPosition(float horizontal, float vertical)
    {
        if (canMove)
        {
            Vector2 position = rigidbody2d.position;
            position.x += character.maxSpeed * horizontal * Time.deltaTime;
            position.y += character.maxSpeed * vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

    }


}
