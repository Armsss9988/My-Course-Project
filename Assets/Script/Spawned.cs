using UnityEngine;

public class Spawned : MonoBehaviour
{
    public Spawner spawner;

    public void MinusSpawn()
    {
        if (spawner != null) spawner.currentEnemies -= 1;
    }
    private void OnDestroy()
    {
        MinusSpawn();
    }
    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        GameManager.OnNewGame += DestroyEnemy;
    }
    private void OnDisable()
    {
        GameManager.OnNewGame -= DestroyEnemy;
    }
}
