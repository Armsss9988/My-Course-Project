using UnityEngine;

[CreateAssetMenu(fileName = "ProjectTile Data", menuName = "Item Data/Projectile Data", order = 1)]
public abstract class ProjectileData : ItemData
{
    public float damage;
    public AudioClip attackSound;
}