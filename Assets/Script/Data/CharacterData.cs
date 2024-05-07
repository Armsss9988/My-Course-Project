using System;
using UnityEngine;
[Serializable]
public class CharacterData
{
    public float health;
    public Vector3 position;

    public CharacterData(float health, Vector3 position)
    {
        this.health = health;
        this.position = position;
    }
}
