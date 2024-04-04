using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ItemData
{
    public float damage;
    public float attackSpeed;
    public AudioClip attackSound;
    public List<AttackTypeTag> attackTypeTags;
}
