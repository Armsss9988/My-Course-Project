using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public class LoadUI : MonoBehaviour
{
    public static LoadUI instance;
    [SerializeField] GameObject loadElementPrefab;
    [SerializeField] GameObject loadContent;

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
    public List<FileItem> SortByDate(List<FileItem> objects)
    {
        return objects.OrderByDescending(x => x.Created).ToList();
    }
    public async Task Refresh()
    {
        for (int i = loadContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(loadContent.transform.GetChild(i).gameObject);
        }
        List<FileItem> files = await CloudSaveManager.instance.ListPlayerFiles();
        List<FileItem> sortedFiles = SortByDate(files);
        foreach (FileItem file in sortedFiles)
        {
            GameObject element = Instantiate(loadElementPrefab, loadContent.transform);
            element.GetComponent<LoadInfoUI>().SetText(file.Key, file.Created.ToString());
        }
    }
    public void CloseLoadUI()
    {
        this.gameObject.SetActive(false);
    }
    private async void OnEnable()
    {
        await Refresh();
        UIManager.OnCloseMenu += CloseLoadUI;
    }
    private void OnDisable()
    {
        UIManager.OnCloseMenu -= CloseLoadUI;
    }
}
