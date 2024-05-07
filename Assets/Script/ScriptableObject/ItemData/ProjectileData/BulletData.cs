using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Item Data/Projectile Data/Bullet Data", order = 1)]
public class BulletData : ProjectileData
{
    public override void Use(Character player)
    {
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