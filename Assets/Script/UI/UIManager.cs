using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip uiSound;
    public static event Action OnOpenDialog;
    public static event Action OnCloseDialog;
    public static event Action OnOpenInventory;
    public static event Action OnCloseInventory;
    public static event Action OnOpenQuestLog;
    public static event Action OnCloseQuestLog;
    public static event Action OnOpenMenu;
    public static event Action OnCloseMenu;
    public static event Action<string> OnQuestClick;
    public static event Action<string> OnChoiceClick;
    public static UIManager instance;

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
        audioSource = GetComponent<AudioSource>();
    }


    public void ChoiceUIClicked(string questName)
    {
        audioSource.PlayOneShot(uiSound);
        OnChoiceClick?.Invoke(questName);
    }

    public void QuestUIClicked(string questName)
    {
        audioSource.PlayOneShot(uiSound);
        OnQuestClick?.Invoke(questName);
    }
    public void OpenDialog()
    {
        Debug.Log("Dialog Open");
        OnOpenDialog?.Invoke();
        CloseInventory();
        CloseQuestLog();
    }
    public void CloseDialog()
    {
        Debug.Log("Dialog Close");
        OnCloseDialog?.Invoke();
    }
    public void OpenInventory()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("Inventory Open");
        OnOpenInventory?.Invoke();
        CloseQuestLog();
        CloseMenu();
    }
    public void CloseInventory()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("Inventory Close");
        OnCloseInventory?.Invoke();
    }
    public void OpenQuestLog()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("QuestLog Open");
        OnOpenQuestLog?.Invoke();
        CloseInventory();
        CloseMenu();
    }
    public void CloseQuestLog()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("QuestLog Open");
        OnCloseQuestLog?.Invoke();
    }
    public void OpenMenu()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("Menu Open");
        OnOpenMenu?.Invoke();
        CloseInventory();
        CloseQuestLog();

    }
    public void CloseMenu()
    {
        audioSource.PlayOneShot(uiSound);
        Debug.Log("Menu Close");
        OnCloseMenu?.Invoke();
    }
    private void OnEnable()
    {
        GameManager.OnLoadGame += CloseMenu;
    }
    private void OnDisable()
    {
        GameManager.OnLoadGame -= CloseMenu;
    }
}
