using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveInfoUI : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject editPanel, Add, Delete;
    [SerializeField] TMP_Text filetext;
    public void OnDeselect(BaseEventData data)
    {
        editPanel.SetActive(false);
    }
    public void SetText(string text)
    {
        filetext.text = text;
    }

    public void OnSelect(BaseEventData eventData)
    {
        editPanel.SetActive(true);
    }
    public async void DeleteSave()
    {
        await GameManager.instance.DeleteSave(filetext.text);
        Destroy(this.gameObject);
    }
    public async void OverrideSave()
    {
        await GameManager.instance.SaveCurrentGame(filetext.text);
        await SaveUI.instance.Refresh();
    }
}
