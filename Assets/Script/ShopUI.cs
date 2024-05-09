using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] List<Item> shopItem;
    [SerializeField] GameObject itemShopPrefab;
    [SerializeField] GameObject itemShopContent;
    [SerializeField] ItemInfoUI itemInfoUI;
    public TMP_Text currentPLayerCoin, currentPlayerWood;
    Character character;

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        CreateItemShop();
        currentPLayerCoin.text = character.GetItemCount(ItemManager.instance.GetItemByName("Coin")).ToString();
        currentPlayerWood.text = character.GetItemCount(ItemManager.instance.GetItemByName("Wood")).ToString();
    }
    void CreateItemShop()
    {
        foreach (Item item in shopItem)
        {
            GameObject itemPrefab = Instantiate(itemShopPrefab, itemShopContent.transform);
            itemPrefab.GetComponent<itemShopUI>().SetItemShopUI(item, this);
        }
    }
}
