using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action OnNewGame;
    public static Action OnSaveGame;
    public static Action OnLoadGame;
    public SaveData saveData;
    public string[] sceneNames;
    bool isSceneLoading = false;
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
        saveData = new();
        DontDestroyOnLoad(this);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
        LoadingScreenUI.instance.OpenLoading();
        StartCoroutine(NewGame());
    }
    public void StartNewGame(Action onNewGameComplete)
    {
        StartCoroutine(NewGame(() => onNewGameComplete?.Invoke()));
    }

    public void SignOut()
    {
        AuthManager.instance.SignOut();
    }
    IEnumerator NewGame(Action onNewGameComplete)
    {
        yield return SceneManager.LoadSceneAsync("Town");
        LoadingScreenUI.instance.CloseLoading();
        onNewGameComplete?.Invoke();
    }
    IEnumerator NewGame()
    {
        yield return SceneManager.LoadSceneAsync("Town");
        LoadingScreenUI.instance.CloseLoading();
    }
    public void UnloadAllScenes()
    {
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
    public void ReloadAllScenes()
    {
        foreach (string sceneName in sceneNames)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
    public IEnumerator LoadAllScenes()
    {
        isSceneLoading = true;
        if (!SceneManager.GetSceneByName("Town").isLoaded)
        {
            yield return SceneManager.LoadSceneAsync("Town");
        }
        foreach (string sceneName in sceneNames)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(sceneName);
            }
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        isSceneLoading = false;
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAndWaitForCompletion(sceneName));
    }
    public IEnumerator LoadSceneAndWaitForCompletion(string sceneName)
    {
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }
        Debug.Log("Scene " + sceneName + " loaded!");
    }
    public async Task LoadGame(string filename)
    {
        LoadingScreenUI.instance.OpenLoading();

        await CloudSaveManager.instance.LoadPlayerSave(filename);
        StartCoroutine(LoadingGame(filename));
    }

    IEnumerator LoadingGame(string filename)
    {
        yield return LoadAllScenes();
        saveData = SaveGame.Load<SaveData>(filename);
        while (isSceneLoading)
        {
            yield return null;
        }
        OnLoadGame?.Invoke();
        LoadingScreenUI.instance.CloseLoading();
    }
    public async Task SaveCurrentGame(string filename)
    {
        saveData = new();
        OnSaveGame?.Invoke();
        SaveGame.Save(filename, saveData);
        await CloudSaveManager.instance.SaveCloud(filename);
    }
    public async Task DeleteSave(string filename)
    {
        SaveGame.Delete(filename);
        await CloudSaveManager.instance.DeletePlayerFile(filename);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        UIManager.OnOpenMenu += PauseGame;
        UIManager.OnCloseMenu += ResumeGame;
    }
    private void OnDisable()
    {
        UIManager.OnOpenMenu -= PauseGame;
        UIManager.OnCloseMenu -= ResumeGame;
    }

}
