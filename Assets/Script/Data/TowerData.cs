public class TowerData
{
    public string towerID;
    public float currentHealth;
    public float currentTimeConstruct;
    public float currentTimeRespawn;
    public bool isDestroyed = false;
    public bool isInConstruct = false;
    public bool isDamagebale = true;

    public TowerData(string towerID, float currentHealth, float currentTimeConstruct, float currentTimeRespawn, bool isDestroyed, bool isInConstruct, bool isDamagebale)
    {
        this.towerID = towerID;
        this.currentHealth = currentHealth;
        this.currentTimeConstruct = currentTimeConstruct;
        this.currentTimeRespawn = currentTimeRespawn;
        this.isDestroyed = isDestroyed;
        this.isInConstruct = isInConstruct;
        this.isDamagebale = isDamagebale;
    }
}
