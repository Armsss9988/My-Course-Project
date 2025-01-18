using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadInfoUI : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject loadPanel;
    [SerializeField] TMP_Text fileText;
    [SerializeField] TMP_Text updateTimeText;
    public void OnDeselect(BaseEventData data)
    {
        loadPanel.SetActive(false);
    }
    public void SetText(string filename, string updateTime)
    {
        fileText.text = filename;
        updateTimeText.text = updateTime;
    }

    public void OnSelect(BaseEventData eventData)
    {
        loadPanel.SetActive(true);
    }

    public void LoadSave()
    {
        GameManager.instance.StartNewGame(async () =>
        {
            await GameManager.instance.LoadGame(fileText.text);
        });
    }
}
