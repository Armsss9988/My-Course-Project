using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    public GameObject questLogPanel;
    public static QuestLogUI instance;
    Character player;
    public GameObject questUIContent, questStepUIContent;
    public GameObject questUIPrefab;
    public GameObject questStepUIPrefab;


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
    void OnEnable()
    {
        UIManager.OnOpenQuestLog += OpenQuestLog;
        UIManager.OnCloseQuestLog += CloseQuestLog;
        UIManager.OnQuestClick += CreateQuestStepUI;
        QuestManager.OnQuestNextStep += CreateQuestStepUI;
        QuestManager.OnQuestStepStateChange += CreateQuestStepUI;
    }
    private void OnDisable()
    {
        UIManager.OnOpenQuestLog -= OpenQuestLog;
        UIManager.OnCloseQuestLog -= CloseQuestLog;
        UIManager.OnQuestClick -= CreateQuestStepUI;
        QuestManager.OnQuestNextStep -= CreateQuestStepUI;
        QuestManager.OnQuestStepStateChange -= CreateQuestStepUI;

    }

    public void CreateQuestUI()
    {
        List<Quest> listQuest = QuestManager.instance.ListQuests;
        foreach (Quest quest in listQuest)
        {
            if (quest != null && quest.questStatus == Quest.QuestStatus.InProgress)
            {
                QuestUI questUI = questUIPrefab.GetComponent<QuestUI>();
                questUI.SetQuestInfo(quest);
                bool isAlready = false;
                foreach (QuestUI currentQuest in questUIContent.GetComponentsInChildren<QuestUI>())
                {
                    if (questUI.questName == currentQuest.questName)
                    {
                        isAlready = true;
                        break;
                    }
                }
                if (!isAlready)
                {
                    GameObject.Instantiate(questUIPrefab, questUIContent.transform);
                }

            }
        }
        foreach (Quest quest in listQuest)
        {
            if (quest != null && quest.questStatus == Quest.QuestStatus.Completed)
            {
                QuestUI questUI = questUIPrefab.GetComponent<QuestUI>();
                questUI.SetQuestInfo(quest);
                bool isAlready = false;
                foreach (QuestUI currentQuest in questUIContent.GetComponentsInChildren<QuestUI>())
                {
                    if (questUI.questName == currentQuest.questName)
                    {
                        isAlready = true;
                        break;
                    }
                }
                if (!isAlready)
                {
                    GameObject.Instantiate(questUIPrefab, questUIContent.transform);
                }

            }
        }
    }
    public void CreateQuestStepUI(string questName)
    {
        if (questStepUIContent.transform.childCount > 0)
        {
            for (int i = questStepUIContent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(questStepUIContent.transform.GetChild(i).gameObject);
            }
        }
        Quest quest = QuestManager.instance.FindQuestByName(questName);
        foreach (QuestStep questStep in quest.QuestBase.QuestSteps)
        {
            if (quest.QuestBase.QuestSteps.IndexOf(questStep) <= quest.currentQuestStepIndex)
            {
                StepUI stepUI = questStepUIPrefab.GetComponent<StepUI>();
                stepUI.SetQuestStepInfo(quest, quest.QuestBase.QuestSteps.IndexOf(questStep));
                GameObject.Instantiate(questStepUIPrefab, questStepUIContent.transform);
            }
        }
    }
    public void CreateQuestStepUI(Quest quest)
    {
        if (questStepUIContent.transform.childCount > 0)
        {
            for (int i = questStepUIContent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(questStepUIContent.transform.GetChild(i).gameObject);
            }
        }
        foreach (QuestStep questStep in quest.QuestBase.QuestSteps)
        {
            if (quest.QuestBase.QuestSteps.IndexOf(questStep) <= quest.currentQuestStepIndex)
            {
                StepUI stepUI = questStepUIPrefab.GetComponent<StepUI>();
                stepUI.SetQuestStepInfo(quest, quest.QuestBase.QuestSteps.IndexOf(questStep));
                GameObject.Instantiate(questStepUIPrefab, questStepUIContent.transform);
            }
        }
    }

    public void OpenQuestLog()
    {

        questLogPanel.SetActive(true);
        CreateQuestUI();
    }
    public void CloseQuestLog()
    {

        questLogPanel.SetActive(false);

    }
    public void ToggleQuestLog()
    {
        if (!questLogPanel.activeSelf)
        {
            UIManager.instance.OpenQuestLog();
        }
        else
        {
            UIManager.instance.CloseQuestLog();
        }
    }
}
