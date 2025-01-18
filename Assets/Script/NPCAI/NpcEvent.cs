using System;
using UnityEngine;

public class NpcEvent
{

    public event Action<GameObject, IEffect> OnBeingHit;
    public event Action<GameObject> OnDetectTarget;
    public event Action<GameObject> OnUndetectTarget;
    public event Action<GameObject> OnTargetInRange;
    public event Action<GameObject> OnTargetOutRange;
    public event Action<GameObject> OnAttack;
    public event Action<GameObject> OnEscape;
    public event Action<GameObject> OnChasing;

    public event Action<GameObject> OnDead;

    public void BeingHit(GameObject attacker, IEffect weapon)
    {
        OnBeingHit?.Invoke(attacker, weapon);
    }
    public void DetectTarget(GameObject target)
    {
        OnDetectTarget?.Invoke(target);
    }
    public void TargetInRange(GameObject target)
    {
        OnTargetInRange?.Invoke(target);
    }
    public void TargetOutRange(GameObject target)
    {
        OnTargetOutRange?.Invoke(target);
    }
    public void Attack(GameObject target)
    {
        OnAttack?.Invoke(target);
    }
    public void Escape(GameObject target)
    {
        OnEscape?.Invoke(target);
    }
    public void Chasing(GameObject target)
    {
        OnChasing?.Invoke(target);
    }
    public void UndetectTarget(GameObject target)
    {
        Debug.Log("Undetect " + target.name);
        OnUndetectTarget?.Invoke(target);
    }
    public void Dead(GameObject attackable)
    {
        OnDead?.Invoke(attackable);
    }
}
