using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Quest;

public class QuestManager : MonoBehaviour
{
    private List<QuestBase> questBases;
    List<Quest> listQuests;
    Character character;
    public GameObject CurrentActiveStepContent;
    public List<Quest> ListQuests => listQuests;

    public static event Action<Quest> OnQuestStarted;
    public static event Action<Quest> OnQuestNextStep;
    public static event Action<Quest> OnQuestFinished;

    public static event Action<Quest> OnQuestStatusChange;
    public static event Action<Quest> OnQuestStepChange;
    public static event Action<Quest> OnQuestStepStateChange;
    public static QuestManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        listQuests = new();
        questBases = Resources.LoadAll<QuestBase>("QuestBase").ToList();
        foreach (QuestBase questBase in questBases)
        {
            listQuests.Add(new Quest(questBase));

        }
        foreach (Quest quest in listQuests)
        {
            Debug.Log("List Quest: " + quest.QuestBase.Name);
        }


    }
    private void Update()
    {
        if (listQuests != null)
        {
            // loop through ALL quests
            foreach (Quest quest in listQuests)
            {
                // if we're now meeting the requirements, switch over to the CAN_START state
                if (quest.questStatus == QuestStatus.RequirementNotMet && IsItemRequirementsMet(quest) && IsQuestRequirementMet(quest))
                {
                    ChangeQuestStatus(quest, QuestStatus.CanStart);
                }
            }
        }

    }

    public List<Quest> FindQuestsWithSameActor(List<Quest> quests, Actor targetActor)
    {
        List<Quest> matchingQuests = new();

        foreach (Quest quest in quests)
        {
            if (quest.QuestBase.Actor.ActorName == targetActor.ActorName)
            {
                matchingQuests.Add(quest);
            }
        }
        Debug.Log("Matching Quest Actor: " + matchingQuests.Count);
        return matchingQuests;
    }
    bool IsItemRequirementsMet(Quest checking)
    {
        foreach (RequirementItem requirementItem in checking.QuestBase.RequirementItems)
        {
            int requirementQuantity = requirementItem.quantity;
            foreach (Inventory.Slot slot in character.inventory.slots)
            {
                Item inventoryItem = ItemManager.instance.GetItemByName(slot.itemName);
                if (inventoryItem == requirementItem.item)
                {
                    int inventoryItemCount = slot.count;
                    requirementQuantity -= inventoryItemCount;
                }
            }
            if (requirementQuantity > 0) return false;
        }
        return true;
    }

    bool IsQuestRequirementMet(Quest checking)
    {
        foreach (QuestBase questBase in checking.QuestBase.RequirementQuestBases)
        {
            foreach (Quest quest in listQuests)
            {
                if (quest.QuestBase == questBase && quest.questStatus != Quest.QuestStatus.Completed) return false;
            }
        }
        return true;
    }
    public Quest FindQuestByName(string questName)
    {
        foreach (Quest quest in listQuests)
        {
            if (quest.QuestBase.Name == questName)
            {
                return quest;
            }
        }
        return null;
    }
    private void ChangeQuestStatus(Quest quest, QuestStatus questStatus)
    {
        quest.questStatus = questStatus;
        OnQuestStatusChange?.Invoke(quest);
    }

    public void StartQuest(Quest quest)
    {
        ChangeQuestStatus(quest, QuestStatus.InProgress);
        OnQuestStarted?.Invoke(quest);
        quest.InstantiateCurrentQuestStep(CurrentActiveStepContent.transform);
        QuestNotification.instance.ToggleNotification(quest.QuestBase.Name, "Start quest " + quest.QuestBase.Name);
    }
    public void NextStepQuest(Quest quest)
    {
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            Debug.Log("Next step quest ");
            quest.InstantiateCurrentQuestStep(CurrentActiveStepContent.transform);
            QuestNotification.instance.ToggleNotification(quest.QuestBase.Name, "Next step " + quest.QuestBase.QuestSteps[quest.currentQuestStepIndex].StepName);
        }
        else
        {
            Debug.Log("Complete quest " + quest.QuestBase.Name);
            CompletedQuest(quest);
        }
        OnQuestNextStep?.Invoke(quest);

    }
    public void CompletedQuest(Quest quest)
    {
        ChangeQuestStatus(quest, QuestStatus.Completed);
        OnQuestFinished?.Invoke(quest);
        if (quest.QuestBase.RewardItem != null)
        {
            character.inventory.Add(quest.QuestBase.RewardItem);
        }
        QuestNotification.instance.ToggleNotification(quest.QuestBase.Name, "Finish quest " + quest.QuestBase.Name);

    }

    public void QuestStepStateChange(Quest quest, int stepIndex, QuestStepState questStepState)
    {

        quest.StoreQuestStepState(questStepState, stepIndex);
        OnQuestStepStateChange?.Invoke(quest);

    }
    private void SaveQuest()
    {
        try
        {
            List<QuestData> questDatas = new();
            foreach (Quest quest in listQuests)
            {
                QuestData questData = quest.GetQuestData();
                questDatas.Add(questData);
            }

            SaveData saveData = GameManager.instance.saveData;
            saveData.questData = questDatas;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void LoadQuest()
    {
        try
        {
            List<Quest> loaddedQuests = new();
            SaveData saveData = GameManager.instance.saveData;
            foreach (QuestData questData in saveData.questData)
            {
                Debug.Log(questData.questName);
                Quest loadQuest = FindQuestByName(questData.questName);

                loadQuest.SetQuestData(questData);
                loaddedQuests.Add(loadQuest);
            }
            listQuests = loaddedQuests;
            foreach (Quest quest in listQuests)
            {
                if (quest.questStatus == Quest.QuestStatus.InProgress)
                {
                    quest.InstantiateCurrentQuestStep(CurrentActiveStepContent.transform);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    private void OnEnable()
    {
        GameManager.OnSaveGame += SaveQuest;
        GameManager.OnLoadGame += LoadQuest;
    }
    private void OnDisable()
    {
        GameManager.OnLoadGame -= LoadQuest;
        GameManager.OnSaveGame -= SaveQuest;
    }

}
