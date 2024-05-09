public class HouseData
{
    public string houseID;
    public float currentHealth;
    public float currentTimeRespawn;
    public bool isDestroyed = false;
    public bool isDamagebale = true;

    public HouseData(string houseID, float currentHealth, float currentTimeRespawn, bool isDestroyed, bool isDamagebale)
    {
        this.houseID = houseID;
        this.currentHealth = currentHealth;
        this.currentTimeRespawn = currentTimeRespawn;
        this.isDestroyed = isDestroyed;
        this.isDamagebale = isDamagebale;
    }
}
