using UnityEngine;
[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    public bool isCollectable = false;
    public bool isHasDelaytime = false;
    public float timeDelay = 1f;
    float delayTimer;

    private void Update()
    {
        if (isHasDelaytime)
        {
            delayTimer -= Time.deltaTime;
        }
        if (delayTimer < 0f)
        {
            isHasDelaytime = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = collision.GetComponent<Character>();
        if (player && isCollectable && !isHasDelaytime)
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                InteractionManager.instance.ItemCollected(item);
                player.inventory.Add(item);
                Destroy(this.gameObject);
                Inventory_UI.instance.Refresh();
                Toolbar_UI.instance.Refresh();
            }
        }
    }
    public CollectableData SaveData()
    {
        return new CollectableData(this.GetComponent<Item>().data.itemName, this.transform.position, this.isCollectable, this.isHasDelaytime, this.delayTimer);
    }
    public void LoadData(CollectableData collectableData)
    {
        this.gameObject.transform.position = collectableData.position;
        this.isCollectable = collectableData.isCollectable;
        this.isHasDelaytime = collectableData.isHasDelaytime;
        this.delayTimer = collectableData.delayTimer;
    }
    private void OnEnable()
    {
        WorldManager.instance.AddItem(this.GetComponent<Item>());
    }
    private void OnDisable()
    {
        WorldManager.instance.RemoveItem(this.GetComponent<Item>());
    }

}

