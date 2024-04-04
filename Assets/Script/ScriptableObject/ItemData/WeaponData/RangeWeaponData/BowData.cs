using UnityEngine;
[CreateAssetMenu(fileName = "Bow Data", menuName = "Item Data/Weapon/Bow Data", order = 1)]
[ExecuteInEditMode]
public class BowData : RangeWeaponData
{
    public override void Use(Character player)
    {
        Debug.Log("Shoot");
        player.GetComponent<CharacterAnimation>().Bow();
        player.GetComponent<CharacterSound>().PlaySound(this.attackSound);
    }
}

