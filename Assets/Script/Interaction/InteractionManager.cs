using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public static event Action OnInteraction;


    public static event Action OnKill;


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
    public void Enemykilled()
    {
        OnKill?.Invoke();
    }
    public void ItemCollected(Item item)
    {
        audioSource.PlayOneShot(pickupSound);
        OnItemCollected?.Invoke(item);
    }
}
