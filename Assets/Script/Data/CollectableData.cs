using UnityEngine;

public class CollectableData
{
    public string itemName;
    public Vector2 position;
    public bool isCollectable;
    public bool isHasDelaytime;
    public float delayTimer;

    public CollectableData(string itemName, Vector2 position, bool isCollectable, bool isHasDelaytime, float delayTimer)
    {
        this.itemName = itemName;
        this.position = position;
        this.isCollectable = isCollectable;
        this.isHasDelaytime = isHasDelaytime;
        this.delayTimer = delayTimer;
    }
}
