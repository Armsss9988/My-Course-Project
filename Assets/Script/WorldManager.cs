using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public GameObject[] enemyType;
    public List<Spawner> spawners;
    public List<Tree> treeData;
    public List<House> houseData;
    public List<Tower> towerData;




    public List<NPCData> enemyData;
    public List<Item> itemData;


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
    public Tree FindTreebyId(string ID)
    {
        foreach (Tree tree in treeData) if (tree.ID == ID) return tree;
        return null;
    }
    public House FindHousebyId(string ID)
    {
        foreach (House house in houseData) if (house.ID == ID) return house;
        return null;
    }
    public Tower FindTowerbyId(string ID)
    {
        foreach (Tower tower in towerData) if (tower.ID == ID) return tower;
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
        saveData.spawnsData = new();
        foreach (Spawner spawner in spawners)
        {
            saveData.spawnsData.Add(spawner.SaveData());
        }
        saveData.houseDatas = new();
        foreach (House house in houseData)
        {
            saveData.houseDatas.Add(house.SaveData());
        }
        saveData.treeDatas = new();
        foreach (Tree tree in treeData)
        {
            saveData.treeDatas.Add(tree.SaveData());
        }
        saveData.towerDatas = new();
        foreach (Tower tower in towerData)
        {
            saveData.towerDatas.Add(tower.SaveData());
        }
        saveData.collectableData = new();
        foreach (Item item in itemData)
        {
            saveData.collectableData.Add(item.GetComponent<Collectable>().SaveData());
        }
        Debug.Log("savedata: " + saveData);
    }

    void ResetGame()
    {
        enemyData.Clear();
        itemData.Clear();
    }
    void OnLoadGame()
    {
        SaveData saveData = GameManager.instance.saveData;
        foreach (EnemyData enemy in saveData.enemyData)
        {
            try
            {
                GameObject ene = FindEnemyPrefabByActorName(enemy.actorName);
                if (ene != null && ene.TryGetComponent<Spawned>(out var spawned) && !ene.TryGetComponent<NPCDialog>(out var npc))
                {

                    GameObject loadEnemy = Instantiate(ene, enemy.position, Quaternion.identity);
                    loadEnemy.GetComponent<NPCData>().currentHealth = enemy.currentHealth;
                    spawned.spawner = FindSpawnerbyId(enemy.spawnerID);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


        }
        foreach (SpawnerData spawnerData in saveData.spawnsData)
        {
            try
            {
                Spawner spawner = FindSpawnerbyId(spawnerData.SpawnerID);
                if (spawner != null)
                {
                    spawner.LoadData(spawnerData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        foreach (TreeData treeData in saveData.treeDatas)
        {
            try
            {
                Tree tree = FindTreebyId(treeData.treeID);
                if (tree != null)
                {
                    tree.LoadData(treeData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        foreach (HouseData houseData in saveData.houseDatas)
        {
            try
            {
                House house = FindHousebyId(houseData.houseID);
                if (house != null)
                {
                    house.LoadData(houseData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        foreach (TowerData towerData in saveData.towerDatas)
        {
            try
            {
                Tower tower = FindTowerbyId(towerData.towerID);
                if (tower != null)
                {
                    tower.LoadData(towerData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        foreach (CollectableData collectableData in saveData.collectableData)
        {
            try
            {
                Item item = Instantiate(ItemManager.instance.GetItemByName(collectableData.itemName), collectableData.position, Quaternion.identity);
                Collectable itemCollectable = item.GetComponent<Collectable>();
                itemCollectable.LoadData(collectableData);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


        }
    }
    public void RemoveEnemy(NPCData gameObject)
    {
        enemyData.Remove(gameObject);
    }
    public void AddEnemy(NPCData gameObject)
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
    public void RemoveItem(Item gameObject)
    {
        itemData.Remove(gameObject);
    }
    public void AddItem(Item gameObject)
    {
        itemData.Add(gameObject);
    }
    public void RemoveTree(Tree gameObject)
    {
        treeData.Remove(gameObject);
    }
    public void AddTree(Tree gameObject)
    {
        treeData.Add(gameObject);
    }
    public void RemoveHouse(House gameObject)
    {
        houseData.Remove(gameObject);
    }
    public void AddHouse(House gameObject)
    {
        houseData.Add(gameObject);
    }
    public void RemoveTower(Tower gameObject)
    {
        towerData.Remove(gameObject);
    }
    public void AddTower(Tower gameObject)
    {
        towerData.Add(gameObject);
    }
    List<EnemyData> EnemyData()
    {
        List<EnemyData> enemies = new();
        foreach (NPCData enemy in enemyData)
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
