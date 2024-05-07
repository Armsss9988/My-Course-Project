using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData : MonoBehaviour
{
    public CharacterData characterData;
    public InventoryData inventoryData;
    public EquipmentData equipmentData;
    public List<EnemyData> enemyData;
}
