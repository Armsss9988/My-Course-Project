using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public AudioClip dead, attack, attackHit, getHit, running;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1.0f);
    }
    public void AttackSound()
    {
        PlaySound(attack);
    }
    public void AttackHitSound()
    {
        PlaySound(attackHit);
    }
    public void GetHitSound()
    {
        PlaySound(getHit);
    }
    public void DeadSound()
    {
        PlaySound(dead);
    }
    public void MovingSound()
    {
        PlaySound(running);
    }
}
