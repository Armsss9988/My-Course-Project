using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    public static Toolbar_UI instance;
    [SerializeField] private List<Slot_UI> toolbarSlots = new();
    public Slot_UI selectedSlot;
    Character player;
    private readonly KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {

        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (player.inventory.slots[i].itemName != "")
            {
                Item item = ItemManager.instance.GetItemByName(player.inventory.slots[i].itemName);
                toolbarSlots[i].SetItem(player.inventory.slots[i], item);
            }
            else
            {
                toolbarSlots[i].SetEmpty();
            }
        }
    }
    public void SelectSlot(int index)
    {
        if (toolbarSlots.Count == 10 && index < 10)
        {
            Item item;
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            if (selectedSlot != toolbarSlots[index])
            {
                selectedSlot = toolbarSlots[index];
                selectedSlot.SetHighlight(true);
                item = ItemManager.instance.GetItemByName(player.inventory.slots[instance.selectedSlot.slot_ID].itemName);
            }
            else
            {
                selectedSlot = null;
                item = null;
            }
            Debug.Log("Item:" + item?.data.itemName);
            player.GetComponent<CharacterSelectedItem>().HandleItemChange(item);
        }
        else
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = null;
            player.GetComponent<CharacterSelectedItem>().HandleItemChange(null);
        }
    }
    public void CheckAlphaNumericKey()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                SelectSlot(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectSlot(9);
        };
    }
}
