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
    [SerializeField] private GameObject highLight;
    public void Awake()
    {
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.BeginDrag, () => inventoryUI.SlotBeginDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.Drag, () => inventoryUI.SlotDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.EndDrag, () => inventoryUI.SlotEndDrag(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.Drop, () => inventoryUI.SlotDrop(this));
        AddTriggerListener(GetComponentInParent<EventTrigger>(), EventTriggerType.PointerClick, () => Toolbar_UI.instance.SelectSlot(slot_ID));
    }
    public void AddTriggerListener(EventTrigger trigger, EventTriggerType eventType, Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
    public void SetHighlight(bool isOn)
    {
        highLight.SetActive(isOn);
    }

}
