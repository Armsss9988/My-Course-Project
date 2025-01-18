using System;
using System.Collections.Generic;
[Serializable]
public class SaveData
{
    public CharacterData characterData;
    public InventoryData inventoryData;
    public EquipmentData equipmentData;
    public List<EnemyData> enemyData;
    public List<SpawnerData> spawnsData;
    public List<CollectableData> collectableData;
    public List<TreeData> treeDatas;
    public List<HouseData> houseDatas;
    public List<TowerData> towerDatas;
    public List<QuestData> questData;
}
