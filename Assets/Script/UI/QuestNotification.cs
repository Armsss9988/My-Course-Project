using System.Collections;
using TMPro;
using UnityEngine;

public class QuestNotification : MonoBehaviour
{
    public static QuestNotification instance;
    public GameObject notificationBackground;
    public TMP_Text questName;
    public TMP_Text description;
    bool isShowing = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void ToggleNotification(string questName, string description)
    {
        StartCoroutine(ShowNotification(questName, description));
    }
    IEnumerator ShowNotification(string questName, string description)
    {
        while (isShowing)
        {
            yield return null;
        }
        isShowing = true;
        this.questName.text = questName;
        this.description.text = description;
        notificationBackground.SetActive(true);
        yield return new WaitForSeconds(3);
        notificationBackground.SetActive(false);
        isShowing = false;
    }

}
