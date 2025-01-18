public class SpawnerData
{
    public string SpawnerID;
    public float currentSpawnTimer;
    public int currentEnemies = 0;
    public SpawnerData(string spawnerID, float currentSpawnTimer, int currentEnemies)
    {
        SpawnerID = spawnerID;
        this.currentSpawnTimer = currentSpawnTimer;
        this.currentEnemies = currentEnemies;
    }
}
