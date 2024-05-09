using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip healSound;
    public static event Action OnInteraction;

    public static event Action OnHealing;
    public static event Action<string> OnPlayerKill;


    public static event Action<Item> OnItemCollected;
    public static InteractionManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
    }

    public void Interaction()
    {
        OnInteraction?.Invoke();
    }
    public void Enemykilled(string actorName)
    {
        OnPlayerKill?.Invoke(actorName);
    }
    public void ItemCollected(Item item)
    {
        audioSource.PlayOneShot(pickupSound);
        OnItemCollected?.Invoke(item);
    }
    public void PlayerHeal()
    {
        audioSource.PlayOneShot(healSound);
        OnHealing?.Invoke();
    }
}
