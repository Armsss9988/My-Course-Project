using UnityEngine;

public interface IDamageable
{

    public bool IsDamageable();
    public void ChangeHealth(float amount);
    public void SourceAttackSound(AudioClip sourceAttackSound);
    public void Force(Vector2 direction, int amount);
}
