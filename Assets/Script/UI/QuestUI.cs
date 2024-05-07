using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public string questName;
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questStatusText;
    public Image questStatusImage;
    public Color activeColor, completedColor;

    public void SetQuestInfo(Quest quest)
    {
        this.questName = quest.QuestBase.Name;
        questNameText.text = quest.QuestBase.Name;
        questDescriptionText.text = quest.QuestBase.Description;
        questStatusText.text = (quest.questStatus == Quest.QuestStatus.InProgress) ? "Activating" : "Completed";
        questStatusImage.GetComponent<Image>().color = (quest.questStatus == Quest.QuestStatus.InProgress) ? activeColor : completedColor;
    }
    public void Awake()
    {
        AddTriggerListener(GetComponent<EventTrigger>(), EventTriggerType.PointerClick, () => UIManager.instance.QuestUIClicked(questName));
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
    public void ChangeQuestUIProgress(Quest quest)
    {
        if (quest.QuestBase.Name == this.questName)
        {
            SetQuestInfo(quest);
        }

    }
    private void OnEnable()
    {
        QuestManager.OnQuestFinished += ChangeQuestUIProgress;
    }
    private void OnDisable()
    {
        QuestManager.OnQuestFinished -= ChangeQuestUIProgress;
    }
}
