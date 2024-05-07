using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Data", menuName = "Item Data/Projectile Data/Arrow Data", order = 1)]
public class ArrowData : ProjectileData
{
    public override void Use(Character player)
    {
        Debug.Log("Using Arrow");
    }
    public override Dictionary<string, string> GetData()
    {
        Dictionary<string, string> data = new()
        {
            { "Damage", damage.ToString() },
        };
        return data;
    }
}