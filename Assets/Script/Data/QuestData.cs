public class QuestData
{
    public string questName;
    public Quest.QuestStatus state;
    public int questStepIndex;
    public QuestStepState[] questStepStates;

    public QuestData(string questName, Quest.QuestStatus state, int questStepIndex, QuestStepState[] questStepStates)
    {
        this.questName = questName;
        this.state = state;
        this.questStepIndex = questStepIndex;
        this.questStepStates = questStepStates;
    }
}
