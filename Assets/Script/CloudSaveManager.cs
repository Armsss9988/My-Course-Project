using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager instance;
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
    public void LoadCloud()
    {
        LoadingScreenUI.instance.OpenLoading();

    }
    public async Task SaveCloud(string filename)
    {
        string json = JsonUtility.ToJson(GameManager.instance.saveData);
        Debug.Log(json);
        var data = new Dictionary<string, object> { { filename, json } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        await SavePlayerFile(filename);
    }
    public async Task SavePlayerFile(string filename)
    {
        byte[] file = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + filename);
        await CloudSaveService.Instance.Files.Player.SaveAsync(filename, file);
    }
    public async Task DeletePlayerFile(string filename)
    {
        await CloudSaveService.Instance.Files.Player.DeleteAsync(filename);
    }
    public async Task LoadPlayerSave(string filename)
    {
        byte[] file = await CloudSaveService.Instance.Files.Player.LoadBytesAsync(filename);
        using FileStream fs = new(Application.persistentDataPath + "/" + filename, FileMode.Create);
        fs.Write(file, 0, file.Length);
    }
    public async Task<List<FileItem>> ListPlayerFiles()
    {
        List<FileItem> files = await CloudSaveService.Instance.Files.Player.ListAllAsync();
        return files;
    }
}
