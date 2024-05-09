using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] GameObject propertyPrefab;
    [SerializeField] GameObject propertyContainer;
    [SerializeField] TMP_Text itemnameInfo;
    [SerializeField] TMP_Text itemDescription;
    public void PopulateItemInformationUI(Item item)
    {
        // Clear existing properties
        for (int i = propertyContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(propertyContainer.transform.GetChild(i).gameObject);
        }

        foreach (var itemdata in item.data.GetData())
        {
            // Get property name and value
            string propertyName = itemdata.Key;
            object propertyValue = itemdata.Value;
            GameObject propertyNameGO = Instantiate(propertyPrefab, propertyContainer.transform);
            propertyNameGO.GetComponent<ItemPropertyInfoUI>().SetText(propertyName, propertyValue.ToString());
        }
        SetItemDescription(item.data.description);
        SetItemInfoName(item.data.itemName);

    }
    void SetItemInfoName(string itemName)
    {
        itemnameInfo.text = itemName;
    }
    void SetItemDescription(string description)
    {
        itemDescription.text = description;
    }
}
