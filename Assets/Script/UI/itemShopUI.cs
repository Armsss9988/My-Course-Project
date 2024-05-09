using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class itemShopUI : MonoBehaviour
{
    Item item;
    [SerializeField] TMP_Text itemNametext;
    [SerializeField] Image itemIcon;
    [SerializeField] ShopUI shop;
    Character character;
    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    public void SetItemShopUI(Item item, ShopUI shopUI)
    {
        this.shop = shopUI;
        this.item = item;
        itemNametext.text = item.data.itemName;
        itemIcon.sprite = item.data.icon;
    }

    public void BuyItem()
    {
        foreach (var item in item.data.requireShop)
        {
        }
    }
}
