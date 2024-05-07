using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepUI : MonoBehaviour
{
    public Quest quest;
    public QuestStep step;
    public TMP_Text stepName;
    public TMP_Text stepDescription;
    public TMP_Text stepStatus;
    public Image stepStatusImage;
    public TMP_Text stepProgress;
    public Color activeColor, completedColor;

    public void SetQuestStepInfo(Quest quest, int index)
    {
        this.quest = quest;
        step = quest.QuestBase.QuestSteps[index];
        stepName.text = quest.QuestBase.QuestSteps[index].StepName;
        stepDescription.text = quest.QuestBase.QuestSteps[index].Description;
        stepStatus.text = (quest.currentQuestStepIndex == index) ? "Activating" : "Completed";
        stepStatusImage.color = (quest.currentQuestStepIndex == index) ? activeColor : completedColor;
        stepProgress.text = quest.questStepStates[index].status;
    }
    /*    private void ChangeStepProgress(Quest quest, int index, QuestStepState questStepState)
        {
            if (quest != null)
            {
                if (quest.QuestBase.Name == this.quest.QuestBase.Name)
                {
                    if ((index == this.step.StepIndex))
                    {
                        stepProgress.text = questStepState.status;
                    }
                }
            }



        }
        private void OnEnable()
        {
            QuestManager.OnQuestStepStateChange += ChangeStepProgress;
        }
        private void OnDisable()
        {
            QuestManager.OnQuestStepStateChange -= ChangeStepProgress;
        }*/
}
