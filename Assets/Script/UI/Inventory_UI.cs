using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public static Inventory_UI instance;
    Character player;
    public List<Slot_UI> slots = new List<Slot_UI>();
    private Slot_UI draggedSlot;
    public bool dragSingle;
    private Image draggedIcon;
    [SerializeField] private Canvas canvas;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Refresh();
        canvas = FindObjectOfType<Canvas>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
        Toolbar_UI.instance.CheckAlphaNumericKey();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
    public void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }

        }
    }
    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[draggedSlot.slot_ID].itemName);
        if (itemToDrop != null)
        {
            if (dragSingle)
            {
                player.GetComponent<CharacterAction>().DropItem(itemToDrop);
                player.inventory.Remove(draggedSlot.slot_ID);
            }
            else
            {
                for (int i = 0; i < player.inventory.slots[draggedSlot.slot_ID].count; i++)
                {
                    player.GetComponent<CharacterAction>().DropItem(itemToDrop);
                }
                player.inventory.Remove(draggedSlot.slot_ID, player.inventory.slots[draggedSlot.slot_ID].count);
            }
            Refresh();
            Toolbar_UI.instance.Refresh();

        }
    }
    public void SlotBeginDrag(Slot_UI slotUI)
    {
        draggedSlot = slotUI;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(40f, 40f);
        draggedIcon.transform.SetParent(canvas.transform);
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Start drag: " + draggedSlot.name);
    }
    public void SlotDrag(Slot_UI slotUI)
    {
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Dragging: " + draggedSlot.name);
    }
    public void SlotEndDrag(Slot_UI slotUI)
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
        Debug.Log("End drag: " + draggedSlot.name);
    }
    public void SlotDrop(Slot_UI slotUI)
    {
        Item itemToMove = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[draggedSlot.slot_ID].itemName);
        int quantiyFromDraggedSlot = player.inventory.slots[draggedSlot.slot_ID].count;
        Item itemFromSlotDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[slotUI.slot_ID].itemName);
        int quantittyFromSlotDrop = player.inventory.slots[slotUI.slot_ID].count;
        if (itemToMove)
        {
            if (player.inventory.slots[slotUI.slot_ID].itemName == "" || player.inventory.slots[slotUI.slot_ID].itemName == player.inventory.slots[draggedSlot.slot_ID].itemName)
            {
                if (dragSingle)
                {
                    player.inventory.slots[slotUI.slot_ID].AddItem(itemToMove);
                    player.inventory.Remove(draggedSlot.slot_ID);
                    Refresh();
                    Toolbar_UI.instance.Refresh();
                }
                else
                {
                    if (draggedSlot != slotUI)
                    {
                        player.inventory.Remove(draggedSlot.slot_ID, quantiyFromDraggedSlot);
                        player.inventory.slots[slotUI.slot_ID].AddItem(itemToMove, quantiyFromDraggedSlot);
                    }
                    Refresh();
                    Toolbar_UI.instance.Refresh();
                }
            }
            else
            {
                if (!dragSingle)
                {
                    player.inventory.Remove(slotUI.slot_ID, player.inventory.slots[slotUI.slot_ID].count);
                    player.inventory.slots[slotUI.slot_ID].AddItem(itemToMove, player.inventory.slots[draggedSlot.slot_ID].count);
                    player.inventory.Remove(draggedSlot.slot_ID, player.inventory.slots[draggedSlot.slot_ID].count);
                    player.inventory.slots[draggedSlot.slot_ID].AddItem(itemFromSlotDrop, quantittyFromSlotDrop);
                    Refresh();
                    Toolbar_UI.instance.Refresh();
                }
            }
        }
        Toolbar_UI.instance.SelectSlot(slotUI.slot_ID);

        Debug.Log("Dropped " + draggedSlot.name + " on " + slotUI.name);

    }
    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }
}
