using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Data", menuName = "Item Data/Material", order = 1)]
public class MaterialData : ItemData
{
    public override void Use(Character player)
    {
    }
    public override Dictionary<string, string> GetData()
    {
        Dictionary<string, string> data = new()
        {
        };
        return data;
    }
}
