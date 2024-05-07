using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public abstract class ArmorData : ItemData
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
    public override Dictionary<string, string> GetData()
    {
        Dictionary<string, string> data = new()
        {
            { "Heath Bonus", healthBonus.ToString() },
            { "Speed Bonus", speedBonus.ToString() },
            { "AS Bonus", attackspeedBonus.ToString() },
        };
        return data;
    }
}
