using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    public static SaveUI instance;
    [SerializeField] GameObject saveElementPrefab;
    [SerializeField] GameObject saveContent;

    private async void Awake()
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
    public async void OnNewSave()
    {
        if (inputField != null && inputField.text.Length > 0)
        {
            await GameManager.instance.SaveCurrentGame(inputField.text);
        }
        await Refresh();
    }
    public async Task Refresh()
    {
        for (int i = saveContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(saveContent.transform.GetChild(i).gameObject);
        }
        List<FileItem> files = await CloudSaveManager.instance.ListPlayerFiles();
        foreach (FileItem file in files)
        {
            GameObject element = Instantiate(saveElementPrefab, saveContent.transform);
            element.GetComponent<SaveInfoUI>().SetText(file.Key);
        }
    }
    private async void OnEnable()
    {
        await Refresh();
    }
}
