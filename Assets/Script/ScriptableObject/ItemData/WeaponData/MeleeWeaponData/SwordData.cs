using UnityEngine;
[CreateAssetMenu(fileName = "Sword Data", menuName = "Item Data/Weapon/Sword Data", order = 1)]
[ExecuteInEditMode]
public class SwordData : MeleeWeaponData
{
    public override void Use(Character player)
    {
        player.GetComponent<CharacterAnimation>().Slash();
        player.GetComponent<CharacterSound>().PlaySound(this.attackSound);
    }
}
