using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator[] animators;
    GameObject hand;
    HandleInput handleInput;
    Vector2 lookDirection = new Vector2(1, 0);
    public bool isAbleToAnimate = true;
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        handleInput = GetComponent<HandleInput>();
        hand = this.transform.Find("Hand").gameObject;
    }
    public Vector3 LookDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    // Update is called once per frame
    public void Slash()
    {
        if (isAbleToAnimate)
        {
            foreach (Animator animator in animators)
            {
                InteractDirection();
                animator.SetTrigger("Slash");
            }
        }
    }
    public void Thrust()
    {
        if (isAbleToAnimate)
        {
            foreach (Animator animator in animators)
            {
                InteractDirection();
                animator.SetTrigger("Thrust");
            }
        }
    }
    public void Bow()
    {
        if (isAbleToAnimate)
        {
            foreach (Animator animator in animators)
            {
                InteractDirection();
                animator.SetTrigger("Bow");
            }
        }
    }
    public void InteractDirection()
    {
        Vector3 clickPosition = LookDirection();
        Vector2 fightDirection = new Vector2(1, 0);
        foreach (Animator animator in animators)
        {
            fightDirection.Set(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y);
            fightDirection.Normalize();
            animator.SetFloat("Fight X", fightDirection.x);
            animator.SetFloat("Fight Y", fightDirection.y);
        }
        float animDirection = Mathf.Abs(fightDirection.y / fightDirection.x);
        hand.transform.localPosition = new Vector3(0, (animDirection >= 1 && fightDirection.y > 0) ? 0.002f : -0.002f);

    }
    public void MovementAnimation(float horizontal, float vertical)
    {
        if (isAbleToAnimate)
        {
            Vector2 move = new Vector2(horizontal, vertical);
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
                hand.transform.localPosition = new Vector3(0, (lookDirection.y >= 0) ? 0.002f : -0.002f);

                foreach (var item in animators)
                {
                    item.SetFloat("Look X", lookDirection.x);
                    item.SetFloat("Look Y", lookDirection.y);
                    item.SetFloat("Speed", move.magnitude);
                }
            }
        }
    }
    public void ToogleAnimator(bool bol)
    {
        foreach (Animator animator in animators)
        {
            animator.enabled = bol;
        }
    }


}
