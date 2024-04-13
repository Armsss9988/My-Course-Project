using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static event Action OnOpenDialog;
    public static event Action OnCloseDialog;
    public static event Action OnOpenInventory;
    public static event Action OnCloseInventory;
    public static event Action OnOpenQuestLog;
    public static event Action OnCloseQuestLog;

    private void Update()
    {

    }



    public void OpenDialog()
    {
        Debug.Log("Dialog Open");
        OnOpenDialog?.Invoke();
    }
    public void CloseDialog()
    {
        Debug.Log("Dialog Close");
        OnCloseDialog?.Invoke();
    }
    public void OpenInventory()
    {
        Debug.Log("Inventory Open");
        OnOpenInventory?.Invoke();
    }
    public void CloseInventory()
    {
        Debug.Log("Inventory Close");
        OnCloseInventory?.Invoke();
    }
    public void OpenQuestLog()
    {
        Debug.Log("QuestLog Open");
        OnOpenQuestLog?.Invoke();
    }
    public void CloseQuestLog()
    {
        Debug.Log("QuestLog Open");
        OnCloseQuestLog?.Invoke();
    }
}
