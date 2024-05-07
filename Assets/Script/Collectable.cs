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

}

