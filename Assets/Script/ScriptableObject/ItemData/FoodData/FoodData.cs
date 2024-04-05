using UnityEngine;
[CreateAssetMenu(fileName = "Food Data", menuName = "Item Data/Food", order = 1)]
public class FoodData : ItemData
{
    public float healingAmount;
    public override void Use(Character player)
    {
        throw new System.NotImplementedException();
    }
}
