using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceUI : MonoBehaviour
{
    public string questName;
    public TMP_Text questionText;
    string actorName;
    public ChoiceState state;
    public DialogueAsset dialogueAsset;

    public enum ChoiceState { StartQuest, FinishStep };



    public void SetInfo(Quest quest, Actor actor, ChoiceState state)
    {
        actorName = actor.ActorName;
        questName = quest.QuestBase.Name;
        questionText.text = quest.QuestBase.Name;
        this.state = state;
        if (state == ChoiceState.StartQuest)
        {
            dialogueAsset = quest.QuestBase.StartDialogue;
        }
    }
    public void SetInfo(Quest quest, Actor actor, ChoiceState state, int stepIndex)
    {
        actorName = actor.ActorName;
        questName = quest.QuestBase.Name;
        questionText.text = quest.QuestBase.Name;
        this.state = state;
        if (state == ChoiceState.FinishStep)
        {
            dialogueAsset = quest.QuestBase.QuestSteps[stepIndex].Dialogue;
        }

    }
    public void Awake()
    {
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerClick, () => UIManager.instance.ChoiceUIClicked(questName));
    }
    public void AddTriggerListener(EventTrigger trigger, EventTriggerType eventType, Action action)
    {
        EventTrigger.Entry entry = new()
        {
            eventID = eventType
        };
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }
    void OnSelected(string questName)
    {
        if (this.questName == questName)
        {
            Quest quest = QuestManager.instance.FindQuestByName(questName);
            if (quest.questStatus == Quest.QuestStatus.CanStart)
            {

                DialogueBoxController.instance.ContinueNewDialog(dialogueAsset);
                QuestManager.instance.StartQuest(quest);

            }
            else if (quest.questStatus == Quest.QuestStatus.InProgress || quest.questStatus == Quest.QuestStatus.Completed)
            {
                DialogueBoxController.instance.ContinueNewDialog(dialogueAsset);
            }
        }

    }


    private void OnEnable()
    {
        UIManager.OnChoiceClick += OnSelected;
    }
    private void OnDisable()
    {
        UIManager.OnChoiceClick -= OnSelected;
    }
}
