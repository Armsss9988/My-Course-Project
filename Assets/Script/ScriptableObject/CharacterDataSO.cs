using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    public float baseHealth = 100f;
    public float bonusHealth = 0f;
    public float baseAttackSpeed = 1f;
    public float bonusAttackSpeed = 1f;
    public float baseSpeed = 1f;
    public float bonusSpeed = 1f;
}
