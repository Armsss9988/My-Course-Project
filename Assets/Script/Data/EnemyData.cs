using System;
using UnityEngine;
[Serializable]
public class EnemyData
{
    public string id;
    public string actorName;
    public float currentHealth;
    public Vector3 position;
    public string? spawnerID;

    public EnemyData(string actorName, float currentHealth, Vector3 position)
    {
        this.actorName = actorName;
        this.currentHealth = currentHealth;
        this.position = position;
    }
    public EnemyData(string actorName, float currentHealth, Vector3 position, string spawnerID)
    {
        this.actorName = actorName;
        this.currentHealth = currentHealth;
        this.position = position;
        this.spawnerID = spawnerID;
    }
}
