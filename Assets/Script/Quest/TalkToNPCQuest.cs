using UnityEngine;

public class TalkToNPCQuest : QuestStep
{
    [SerializeField]
    private int current = 0;
    [SerializeField]
    private int required = 5;
    protected override void SetQuestStepState(string state)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        UpdateState();
    }
    private void OnEnable()
    {
        UIManager.OnChoiceClick += TalkNpcforQuest;
    }

    private void OnDisable()
    {
        UIManager.OnChoiceClick -= TalkNpcforQuest;
    }

    private void TalkNpcforQuest(string questName)
    {
        if (this.quest.QuestBase.Name == questName)
        {
            if (current < required)
            {
                current++;
                UpdateState();
            }

            if (current >= required)
            {
                FinishQuestStep();
            }
        }

    }

    private void UpdateState()
    {
        string state = current.ToString();
        string status = "Talk with " + Actor.ActorName + " " + current + " / " + required + " ";
        ChangeState(state, status);
    }
}
