using UnityEngine;

public class DropItem : MonoBehaviour
{
    [System.Serializable]
    public class ObjectScale
    {
        [SerializeField]
        public GameObject objects;
        [SerializeField]
        [Range(1, 100)]
        public int dropChange;
        [SerializeField]
        [Range(1, 100)]
        public int maxDrop;

    }

    [SerializeField]
    public ObjectScale[] objectsScale;

    public void OnDropItem()
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = new(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        foreach (var item in objectsScale)
        {
            if (Random.value <= item.dropChange)
            {
                int dropTotal = Random.Range(1, item.maxDrop + 1);
                for (int i = 0; i < dropTotal; i++)
                {
                    Instantiate(item.objects, spawnOffset + spawnLocation, Quaternion.identity);
                }
            }
        }
    }
}
