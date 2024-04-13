using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [SerializeField] private Actor actor;
    [SerializeField] string stepName;
    [SerializeField] string description;
    [SerializeField] int stepIndex;
    [SerializeField] DialogueAsset startDialogue;
    [SerializeField] DialogueAsset inProgressDialogue;
    [SerializeField] DialogueAsset completeDialogue;
    private bool isFinished = false;
    Quest quest;
    enum StepType { Dialogue, CheckItem, GiveItem, RewardItem }
    enum StepStatus { None, InProgress, Completed }


    public string Name => name;
    public string Description => description;
    public int StepIndex => stepIndex;
    public DialogueAsset StartDialogue => startDialogue;
    public DialogueAsset InPogressDiagloue => inProgressDialogue?.dialogue?.Length > 0 ? inProgressDialogue : startDialogue;
    public DialogueAsset CompleteDialogue => completeDialogue;




    public void InitializeQuestStep(Quest quest, int stepIndex)
    {
        this.quest = quest;
        this.stepIndex = stepIndex;
    }

    protected void FinishQuestStep()
    {
        if (!this.isFinished)
        {
            this.isFinished = true;
            GameManager.instance.questManager.NextStepQuest(this.quest);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        GameManager.instance.questManager.QuestStepStateChange(
            quest,
            stepIndex,
            new QuestStepState(newState, newStatus)
        );
    }

    protected abstract void SetQuestStepState(string state);


}
