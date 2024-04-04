using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour
{
    CharacterMovement characterMovement;
    public GameObject hitbox;
    void Start()
    {
        characterMovement = GetComponentInParent<CharacterMovement>();
    }

    public void DisableMovement()
    {
        characterMovement.canMove = false;
    }
    public void EnableMovement()
    {
        characterMovement.canMove = true;
    }
    public void EnableHitBox()
    {
        hitbox.SetActive(true);
    }
    public void DisablehitBox()
    {
        hitbox.SetActive(false);
    }
}
