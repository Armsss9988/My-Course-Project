using UnityEngine;
using UnityEngine.U2D.Animation;

public class ArmorData : ItemData
{
    public SpriteLibraryAsset libraryAsset;
    public Color color;
    public float healthBonus;
    public float speedBonus;
    public float attackspeedBonus;
    public override void Use(Character player)
    {
        throw new System.NotImplementedException();
    }
}
