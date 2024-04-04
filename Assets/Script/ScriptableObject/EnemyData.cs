using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public int maxHealth = 10;
    public int damage = 1;
    public float detectRange;
    public float undetectRange;
    public float minAttackZone;
    public float maxAttackZone;
    public float speed;
    public float attackSpeed;
    public AudioClip attackSound;
    public AudioClip beingHitSound;
    public AudioClip deadSound;
    public GameObject bullet;
}
