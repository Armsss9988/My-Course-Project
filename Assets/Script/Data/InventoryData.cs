using System;
using System.Collections.Generic;
using static Inventory;
[Serializable]
public class InventoryData
{
    public List<Slot> slots;

    public InventoryData(List<Slot> slots)
    {
        this.slots = slots;
    }
}
