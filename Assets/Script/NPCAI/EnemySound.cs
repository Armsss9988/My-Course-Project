using UnityEngine;

public class EnemySound : MonoBehaviour
{
    AudioSource audioSource;
    NPCData enemy;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemy = GetComponent<NPCData>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1.5f);
    }
    public void AttackSound()
    {
        PlaySound(enemy.attackSound);
    }
    public void AttackWeaponSound()
    {
        PlaySound(enemy.weaponAttackSound);
    }
    public void AttackWeaponHitSound()
    {
        PlaySound(enemy.weaponHitSound);
    }
    public void GetHitSound()
    {
        PlaySound(enemy.beingHitSound);
    }
    public void DeadSound()
    {
        PlaySound(enemy.deadSound);
    }

}
