using TMPro;
using UnityEngine;

public class ItemPropertyInfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text Property;
    [SerializeField] TMP_Text Value;
    public void SetText(string property, string value)
    {
        this.Property.text = property;
        this.Value.text = value;

    }
}
