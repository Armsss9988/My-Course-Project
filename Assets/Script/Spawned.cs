using UnityEngine;

public class Spawned : MonoBehaviour
{
    public Spawner spawner;

    public void MinusSpawn()
    {
        spawner.currentEnemies -= 1;
    }
    private void OnDestroy()
    {
        MinusSpawn();
    }
}
