using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    public int slot_ID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public Inventory_UI inventoryUI;
    Item item;
    [SerializeField] private GameObject highLight;
    [SerializeField] private GameObject infoPanel;
    public UIType type;
    public enum UIType
    {
        All,
        Torso,
        Pant,
        Shoes,
        Gloves,
        Shield,
        Arrow,
    }
    public void Awake()
    {
        /*infoPanel = this.transform.Find("InfoPanel").gameObject;*/
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.BeginDrag, () => inventoryUI.SlotBeginDrag(this));
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.Drag, () => inventoryUI.SlotDrag(this));
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.EndDrag, () => inventoryUI.SlotEndDrag(this));
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.Drop, () => inventoryUI.SlotDrop(this));
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerClick, () => Toolbar_UI.instance.SelectSlot(slot_ID));
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerClick, () => this.SetPanelInfo());
        /*        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerExit, () => this.DeactiveInfoPanel());*/
    }
    public void AddTriggerListener(EventTrigger trigger, EventTriggerType eventType, Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }
    public void SetItem(Inventory.Slot slot, Item item)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            this.item = item;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();

        }
    }
    public void SetEmpty()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
    public void SetHighlight(bool isOn)
    {
        highLight.SetActive(isOn);
    }
    /*    public void ActiveInfoPanel()
        {
            if (this.item != null)
            {
                SetItemInfo(item);
                infoPanel.SetActive(true);
            }
        }
        public void DeactiveInfoPanel()
        {
            if (this.item != null)
            {
                SetItemInfo(item);
                infoPanel.SetActive(false);
            }

        }*/
    public void SetItemInfo(Item item)
    {

        TMP_Text nameText = infoPanel.transform.Find("Item Name").gameObject.GetComponent<TMP_Text>();
        TMP_Text infoText = infoPanel.transform.Find("Item Info").gameObject.GetComponent<TMP_Text>();
        if (item != null)
        {
            nameText.text = item.data.itemName;
            if (item.data is WeaponData weaponData)
            {
                infoText.text = "Damage: " + weaponData.damage
                    + "\nAttack Speed: " + weaponData.attackSpeed;
            }
            else if (item.data is ArmorData armorData)
            {
                infoText.text = "Heath: " + armorData.healthBonus
                    + "\nAttack Speed: " + "+ " + armorData.attackspeedBonus + "%"
                    + "\nMovement Speed: " + "+ " + armorData.speedBonus + "%";
            }
        }
    }
    public void SetPanelInfo()
    {
        infoPanel.GetComponent<ItemInfoUI>().PopulateItemInformationUI(this.item);
    }

}
