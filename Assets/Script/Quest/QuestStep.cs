using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [SerializeField] private Actor actor;
    [SerializeField] string stepName;
    [SerializeField] string description;
    [SerializeField] int stepIndex;
    [SerializeField] DialogueAsset dialogue;

    private bool isFinished = false;
    public Quest quest;
    enum StepType { Dialogue, CheckItem, GiveItem, RewardItem }
    enum StepStatus { None, InProgress, Completed }


    public string StepName => stepName;
    public string Description => description;
    public int StepIndex => stepIndex;
    public Actor Actor => actor;
    public DialogueAsset Dialogue => dialogue;




    public void InitializeQuestStep(Quest quest, int stepIndex, string questStepState)
    {
        this.quest = quest;
        this.stepIndex = stepIndex;
        if (questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    protected void FinishQuestStep()
    {
        if (!this.isFinished)
        {
            this.isFinished = true;
            Quest questGet = QuestManager.instance.FindQuestByName(this.quest.QuestBase.Name);
            QuestNotification.instance.ToggleNotification(quest.QuestBase.Name, "You have finish step " + this.StepName);
            QuestManager.instance.NextStepQuest(questGet);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        QuestManager.instance.QuestStepStateChange(
            quest,
            stepIndex,
            new QuestStepState(newState, newStatus)
        );
    }

    protected abstract void SetQuestStepState(string state);


}
