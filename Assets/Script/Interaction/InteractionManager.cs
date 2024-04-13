using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static event Action OnInteraction;


    public static event Action OnKill;


    public static event Action<Item> OnItemCollected;

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
        OnItemCollected?.Invoke(item);
    }
}
