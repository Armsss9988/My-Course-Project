using System.Collections;
using TMPro;
using UnityEngine;

public class KillNotification : MonoBehaviour
{
    [SerializeField] TMP_Text actorName;
    [SerializeField] GameObject notificationPanel;
    bool isShowing = false;
    public static KillNotification instance;

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
    public void SetNotification(string name)
    {
        this.actorName.text = name;
    }
    public void ToggleNotification(string name)
    {
        StartCoroutine(ShowNotification(name));
    }
    IEnumerator ShowNotification(string name)
    {
        if (isShowing)
        {
            yield return null;
        }
        isShowing = true;
        notificationPanel.SetActive(true);
        SetNotification(name);
        yield return new WaitForSeconds(5f);
        notificationPanel.SetActive(false);
        isShowing = false;
    }
    private void OnEnable()
    {
        InteractionManager.OnPlayerKill += ToggleNotification;
    }
    private void OnDisable()
    {
        InteractionManager.OnPlayerKill -= ToggleNotification;
    }
}
