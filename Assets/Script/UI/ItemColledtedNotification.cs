using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemColledtedNotification : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] GameObject notificationPanel;
    bool isShowing = false;
    public static ItemColledtedNotification instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void SetNotification(Sprite sprite, string name)
    {
        this.icon.sprite = sprite;
        this.itemName.text = name;
    }
    public void ToggleNotification(Item item)
    {
        StartCoroutine(ShowNotification(item));
    }
    IEnumerator ShowNotification(Item item)
    {
        if (isShowing)
        {
            yield return null;
        }
        isShowing = true;
        notificationPanel.SetActive(true);
        SetNotification(item.data.icon, item.data.itemName);
        yield return new WaitForSeconds(5f);
        notificationPanel.SetActive(false);
        isShowing = false;
    }
    private void OnEnable()
    {
        InteractionManager.OnItemCollected += ToggleNotification;
    }
    private void OnDisable()
    {
        InteractionManager.OnItemCollected -= ToggleNotification;
    }
}
