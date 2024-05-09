using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Character character;
    public bool canMove = true;
    CharacterSound sound;
    AudioSource audioSource;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        sound = GetComponent<CharacterSound>();
        audioSource = GetComponent<AudioSource>();
    }

    public void MovementPosition(float horizontal, float vertical)
    {
        if (canMove)
        {
            Vector2 position = rigidbody2d.position;
            position.x += character.maxSpeed * horizontal * Time.deltaTime;
            position.y += character.maxSpeed * vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = sound.running;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }

    }
    void StopMoving()
    {
        canMove = false;
    }
    void StartMoving()
    {
        canMove = true;
    }
    private void OnEnable()
    {
        UIManager.OnOpenDialog += StopMoving;
        UIManager.OnCloseDialog += StartMoving;
    }
    private void OnDisable()
    {
        UIManager.OnOpenDialog -= StopMoving;
        UIManager.OnCloseDialog -= StartMoving;
    }


}
