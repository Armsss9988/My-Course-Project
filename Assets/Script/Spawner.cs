using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectScale
    {
        [SerializeField]
        public GameObject objects;
        [SerializeField]
        [Range(1, 100)]
        public int spawnChange;

    }

    [SerializeField]
    public bool isSpawning = true;
    [SerializeField] public string id;
    [ContextMenu("Generate id")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public GameObject defaulEnemy;
    public ObjectScale[] randomEnemies;
    float currentSpawnTimer = 0f;
    public float delay = 5f;
    public int maxEnemies = 10;
    public int currentEnemies = 0;
    public string ID => id;


    private void Update()
    {
        if (isSpawning)
        {
            currentSpawnTimer += Time.deltaTime;
            if (currentSpawnTimer >= delay)
            {
                currentSpawnTimer = 0f;
                SpawnEnemy();
            }
        }
    }
    public void LoadData(SpawnerData spawnerData)
    {
        this.currentEnemies = spawnerData.currentEnemies;
        this.currentSpawnTimer = spawnerData.currentSpawnTimer;
    }
    public SpawnerData SaveData()
    {
        return new SpawnerData(this.id, this.currentSpawnTimer, this.currentEnemies);
    }

    void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies && isSpawning)
        {
            foreach (ObjectScale enemyType in randomEnemies)
            {
                if (enemyType.spawnChange > Random.Range(0, 100))
                {
                    GameObject enemy1 = Instantiate(enemyType.objects, transform.position, Quaternion.identity);
                    enemy1.layer = this.gameObject.layer;
                    enemy1.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponent<SpriteRenderer>().sortingLayerName;
                    foreach (SpriteRenderer sp in enemy1.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sp.sortingLayerName = this.GetComponent<SpriteRenderer>().sortingLayerName;
                        sp.gameObject.layer = this.gameObject.layer;
                    }
                    enemy1.TryGetComponent<Spawned>(out var spawned1); spawned1.spawner = this;
                    currentEnemies++;
                    return;
                }
            }
            GameObject enemy0 = Instantiate(defaulEnemy, transform.position, Quaternion.identity);
            enemy0.TryGetComponent<Spawned>(out var spawned); spawned.spawner = this;
            currentEnemies++;
        }
    }
    void ResetGame()
    {
        currentEnemies = 0;
        currentSpawnTimer = 0f;
    }
    private void OnEnable()
    {
        WorldManager.instance.AddSpawner(this);
    }
    private void OnDisable()
    {
        WorldManager.instance.RemoveSpawner(this);
    }
}
