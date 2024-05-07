using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ItemData
{
    public float damage;
    public float attackSpeed;
    public AudioClip attackSound;
    public List<AttackTypeTag> attackTypeTags;

    public override Dictionary<string, string> GetData()
    {
        Dictionary<string, string> data = new()
        {
            { "Attack Speed", attackSpeed.ToString() },
            { "Damage", damage.ToString() },
        };
        return data;
    }
}
