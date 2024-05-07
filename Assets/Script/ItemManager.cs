using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;
    private Dictionary<string, Item> nameToItemDict = new();
    public static ItemManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        foreach (Item item in items)
        {
            AddItem(item);
        }
    }
    private void AddItem(Item item)
    {
        if (!nameToItemDict.ContainsKey(item.data.itemName))
        {
            nameToItemDict.Add(item.data.itemName, item);
        }
    }
    public Item GetItemByName(string key)
    {
        try
        {
            if (nameToItemDict.ContainsKey(key))
            {
                return nameToItemDict[key];
            }
            return null;
        }
        catch { return null; }
    }
}
