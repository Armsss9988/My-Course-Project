using System.Collections.Generic;
using UnityEngine;
public abstract class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public string description;
    public Dictionary<Item, int> requireShop;
    public abstract void Use(Character player);
    public abstract Dictionary<string, string> GetData();

}

