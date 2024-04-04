using UnityEngine;
[CreateAssetMenu(fileName = "Axe Data", menuName = "Item Data/Weapon/Axe Data", order = 1)]
[ExecuteInEditMode]
public class AxeData : WeaponData
{
    public override void Use(Character player)
    {
        Animator animator = player.gameObject.GetComponent<Animator>();
        animator.SetTrigger("Hit");
    }
}
