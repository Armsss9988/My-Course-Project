using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public GameObject[] enemyType;
    public List<Spawner> spawners;
    public List<Enemy> enemyData;


    public GameObject FindEnemyPrefabByActorName(string name)
    {
        foreach (GameObject enemy in enemyType)
        {
            if (enemy.GetComponent<Actor>().ActorName == name)
            {
                return enemy;
            }
        }
        return null;
    }
    public Spawner FindSpawnerbyId(string ID)
    {
        foreach (Spawner spawner in spawners) if (spawner.ID == ID) return spawner;
        return null;
    }

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
    }

    void OnSaveGame()
    {
        SaveData saveData = GameManager.instance.saveData;
        saveData.enemyData = EnemyData();
        Debug.Log("savedata: " + saveData);
    }

    void ResetGame()
    {
        enemyData.Clear();
    }
    void OnLoadGame()
    {
        SaveData saveData = GameManager.instance.saveData;
        foreach (EnemyData enemy in saveData.enemyData)
        {
            GameObject ene = FindEnemyPrefabByActorName(enemy.actorName);
            if (ene != null && ene.TryGetComponent<Spawned>(out var spawned))
            {

                GameObject loadEnemy = Instantiate(ene, enemy.position, Quaternion.identity);
                loadEnemy.GetComponent<Enemy>().currentHealth = enemy.currentHealth;
                spawned.spawner = FindSpawnerbyId(enemy.spawnerID);
            }

        }
    }
    public void RemoveEnemy(Enemy gameObject)
    {
        enemyData.Remove(gameObject);
    }
    public void AddEnemy(Enemy gameObject)
    {
        enemyData.Add(gameObject);
    }
    public void RemoveSpawner(Spawner gameObject)
    {
        spawners.Remove(gameObject);
    }
    public void AddSpawner(Spawner gameObject)
    {
        spawners.Add(gameObject);
    }
    List<EnemyData> EnemyData()
    {
        List<EnemyData> enemies = new();
        foreach (Enemy enemy in enemyData)
        {
            if (enemy.TryGetComponent<Spawned>(out var spawned))
            {
                enemies.Add(new EnemyData(enemy.GetComponent<Actor>().ActorName, enemy.currentHealth, enemy.transform.position, spawned.spawner != null ? spawned.spawner.id : null));
            }
            else
            {
                enemies.Add(new EnemyData(enemy.GetComponent<Actor>().ActorName, enemy.currentHealth, enemy.transform.position));
            }

        }
        return enemies;
    }
    private void OnEnable()
    {
        GameManager.OnNewGame += ResetGame;
        GameManager.OnSaveGame += OnSaveGame;
        GameManager.OnLoadGame += OnLoadGame;
    }
    private void OnDisable()
    {
        GameManager.OnNewGame -= ResetGame;
        GameManager.OnSaveGame -= OnSaveGame;
        GameManager.OnLoadGame -= OnLoadGame;
    }
}
