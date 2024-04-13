using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    public GameObject questLogPanel;
    public static QuestLogUI instance;
    Character player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
    }
    /*    void OnEnable()
        {
            UIManager.OnOpenInventory += CloseQuestLog;
            UIManager.OnOpenDialog += CloseQuestLog;
            UIManager.OnOpenQuestLog += OpenQuestLog;
        }*/
    public void OpenQuestLog()
    {
        questLogPanel.SetActive(true);
    }
    public void CloseQuestLog()
    {

        questLogPanel.SetActive(false);

    }
    /*    public void ToggleQuestLog()
        {
            if (!questLogPanel.activeSelf)
            {
                GameManager.instance.uiManager.OpenInventory();
            }
            else
            {
                GameManager.instance.uiManager.CloseInventory();
            }
        }*/
}
