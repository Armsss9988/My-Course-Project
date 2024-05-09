using UnityEngine;

public class Quest
{
    private QuestBase questBase;
    public QuestStatus questStatus;
    public int currentQuestStepIndex;
    public QuestStepState[] questStepStates;



    public QuestBase QuestBase => questBase;

    public enum QuestStatus { RequirementNotMet, CanStart, InProgress, Completed };

    public Quest(QuestBase questBase, QuestStatus questStatus, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.questBase = questBase;
        this.questStatus = questStatus;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

    }
    public Quest(QuestBase questBase)
    {
        this.questBase = questBase;
        this.questStatus = QuestStatus.RequirementNotMet;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[questBase.QuestSteps.Count];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(questBase.Name, questStatus, currentQuestStepIndex, questStepStates);
    }
    public void SetQuestData(QuestData questData)
    {
        this.questStatus = questData.state;
        this.currentQuestStepIndex = questData.questStepIndex;
        this.questStepStates = questData.questStepStates;
    }
    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < questBase.QuestSteps.Count;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = GameObject.Instantiate(questBase.QuestSteps[currentQuestStepIndex], parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(this, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
            questStepStates[stepIndex].status = questStepState.status;
        }
        else
        {
            Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: "
                + "Quest = " + questBase.Name + ", Step Index = " + stepIndex);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = questBase.QuestSteps[currentQuestStepIndex].gameObject;
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: QuestId=" + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

}
