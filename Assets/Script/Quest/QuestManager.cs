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

    public List<Quest> ListQuests => listQuests;

    public static event Action<Quest> OnQuestStarted;
    public static event Action<Quest> OnQuestNextStep;
    public static event Action<Quest> OnQuestFinished;

    public static event Action<Quest> OnQuestStatusChange;
    public static event Action<Quest> OnQuestStepChange;
    public static event Action<Quest, int, QuestStepState> OnQuestStepStateChange;


    void Awake()
    {

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
                Item inventoryItem = GameManager.instance.itemManager.GetItemByName(slot.itemName);
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
    private void ChangeQuestStatus(Quest quest, QuestStatus questStatus)
    {
        quest.questStatus = questStatus;
        OnQuestStatusChange?.Invoke(quest);
    }

    public void StartQuest(Quest quest)
    {
        OnQuestStarted?.Invoke(quest);
        ChangeQuestStatus(quest, QuestStatus.InProgress);
        quest.InstantiateCurrentQuestStep(this.transform);
    }
    public void NextStepQuest(Quest quest)
    {
        OnQuestNextStep?.Invoke(quest);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            CompletedQuest(quest);
        }
    }
    public void CompletedQuest(Quest quest)
    {
        OnQuestFinished?.Invoke(quest);
        ChangeQuestStatus(quest, QuestStatus.Completed);
    }

    public void QuestStepStateChange(Quest quest, int stepIndex, QuestStepState questStepState)
    {
        OnQuestStepStateChange?.Invoke(quest, stepIndex, questStepState);
    }

}
