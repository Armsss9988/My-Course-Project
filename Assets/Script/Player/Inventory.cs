using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string itemName;
        public Sprite icon;
        public int count;
        public int maxAllowed;
        public Type type;
        public enum Type
        {
            All,
            Torso,
            Pant,
            Shoes,
            Gloves,
            Shield,
            Arrow,
        }
        public Slot()
        {
            type = Type.All;
            itemName = "";
            count = 0;
            maxAllowed = 99;
        }
        public bool CanAddItem()
        {
            if (count < maxAllowed)
            {
                return true;
            }
            return false;
        }
        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
            count++;
        }
        public void AddItem(Item item, int numToAdd)
        {
            if (this.count + numToAdd <= maxAllowed)
            {
                this.itemName = item.data.itemName;
                this.icon = item.data.icon;
                count += numToAdd;
            }

        }
        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }

        }
    }
    public List<Slot> slots = new List<Slot>();
    public Inventory(int numSlot)
    {
        for (int i = 0; i < (numSlot - 6); i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        Slot slotTorso = new Slot();
        slotTorso.type = Slot.Type.Torso;
        slots.Add(slotTorso);
        Slot slotPant = new Slot();
        slotPant.type = Slot.Type.Pant;
        slots.Add(slotPant);
        Slot slotShoes = new Slot();
        slotShoes.type = Slot.Type.Shoes;
        slots.Add(slotShoes);
        Slot slotGloves = new Slot();
        slotGloves.type = Slot.Type.Gloves;
        slots.Add(slotGloves);
        Slot slotShield = new Slot();
        slotShield.type = Slot.Type.Shield;
        slots.Add(slotShield);
        Slot slotArrow = new Slot();
        slotArrow.type = Slot.Type.Arrow;
        slots.Add(slotArrow);
    }
    public void Add(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.CanAddItem() && IsCorrectSlot(slot, item))
            {
                slot.AddItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {

            if (slot.itemName == "" && IsCorrectSlot(slot, item))
            {
                slot.AddItem(item);
                return;
            }
        }
    }
    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }
    bool IsCorrectSlot(Slot slot, Item item)
    {
        if (slot.type == Slot.Type.All)
        {
            return true;
        }
        if (slot.type == Slot.Type.Torso)
        {
            if (item.data is not TorsoData)
            {
                return false;
            }
            else return true;
        }
        if (slot.type == Slot.Type.Pant)
        {
            if (item.data is not PantData)
            {
                return false;
            }
            else return true;
        }
        if (slot.type == Slot.Type.Shoes)
        {
            if (item.data is not ShoesData)
            {
                return false;
            }
            else return true;
        }
        if (slot.type == Slot.Type.Gloves)
        {
            if (item.data is not GlovesData)
            {
                return false;
            }
            else return true;
        }
        if (slot.type == Slot.Type.Shield)
        {
            if (item.data is not WeaponData)
            {
                return false;
            }
            else return true;
        }
        if (slot.type == Slot.Type.Arrow)
        {
            if (item.data is not ArrowData)
            {
                return false;
            }
            else return true;
        }
        return true;
    }
}
