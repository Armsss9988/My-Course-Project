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
    public GameObject defaulEnemy;
    public ObjectScale[] randomEnemies;
    public float delay = 5f;
    public int maxEnemies = 10;
    public int currentEnemies = 0;


    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), delay, delay);
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
                    enemy1.AddComponent<Spawned>().spawner = this;
                    currentEnemies++;
                    return;
                }
            }
            GameObject enemy0 = Instantiate(defaulEnemy, transform.position, Quaternion.identity);
            enemy0.AddComponent<Spawned>().spawner = this;
            currentEnemies++;
        }
    }
}
