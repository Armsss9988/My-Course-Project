using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class QuestBase : ScriptableObject
{
    [SerializeField] Actor actor;
    [SerializeField] string questName;
    [SerializeField] string description;
    [SerializeField] DialogueAsset startDialogue;
    [SerializeField] DialogueAsset inProgressDialogue;
    [SerializeField] DialogueAsset completeDialogue;
    [SerializeField] List<QuestStep> questSteps;
    [SerializeField] List<QuestBase> requirementQuests;
    [SerializeField] List<RequirementItem> requirementItems;
    [SerializeField] Item rewardItem;

    public string Name => questName;

    public Actor Actor => actor;
    public string Description => description;
    public DialogueAsset StartDialogue => startDialogue;
    public DialogueAsset InPogressDiagloue => inProgressDialogue?.dialogue?.Length > 0 ? inProgressDialogue : startDialogue;
    public DialogueAsset CompleteDialogue => completeDialogue;

    public List<QuestStep> QuestSteps => questSteps;
    public List<QuestBase> RequirementQuestBases => requirementQuests;
    public List<RequirementItem> RequirementItems => requirementItems;

    public Item RewardItem => rewardItem;
}
