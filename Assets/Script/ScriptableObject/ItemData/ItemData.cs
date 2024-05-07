using System.Collections.Generic;
using UnityEngine;
public abstract class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public abstract void Use(Character player);
    public abstract Dictionary<string, string> GetData();

}

