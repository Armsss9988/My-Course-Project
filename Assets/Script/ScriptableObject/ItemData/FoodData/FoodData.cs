using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Food Data", menuName = "Item Data/Food", order = 1)]
public class FoodData : ItemData
{
    public float healingAmount;

    public override Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>();
    }

    public override void Use(Character player)
    {
        player.ChangeHealth(healingAmount);
    }
}
