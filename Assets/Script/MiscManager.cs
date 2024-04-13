using System;
using UnityEngine;

public class MiscManager : MonoBehaviour
{
    public static event Action OnCoinCollected;

    public void CollectedCoin()
    {
        OnCoinCollected?.Invoke();
    }
}
