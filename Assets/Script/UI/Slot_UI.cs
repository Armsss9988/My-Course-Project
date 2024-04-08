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
        infoPanel = this.transform.Find("InfoPanel").gameObject;
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.BeginDrag, () => inventoryUI.SlotBeginDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.Drag, () => inventoryUI.SlotDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.EndDrag, () => inventoryUI.SlotEndDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.Drop, () => inventoryUI.SlotDrop(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.PointerClick, () => Toolbar_UI.instance.SelectSlot(slot_ID));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.PointerEnter, () => this.ActiveInfoPanel());
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.PointerExit, () => this.DeactiveInfoPanel());
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
            SetItemInfo(item);
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
    public void ActiveInfoPanel()
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

    }
    public void SetItemInfo(Item item)
    {
        // Kích hoạt object bảng thông tin

        TMP_Text nameText = infoPanel.transform.Find("Item Name").gameObject.GetComponent<TMP_Text>();
        TMP_Text infoText = infoPanel.transform.Find("Item Info").gameObject.GetComponent<TMP_Text>();
        if (item != null)
        {
            // Hiển thị tên item
            nameText.text = item.data.itemName;

            // Kiểm tra loại item và hiển thị thông tin đặc biệt
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

}
