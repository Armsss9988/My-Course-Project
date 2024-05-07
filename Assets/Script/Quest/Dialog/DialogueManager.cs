using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static event Action<string> OnQuestChoiceClicked;


    public void ChoiceQuest(string questName)
    {
        OnQuestChoiceClicked?.Invoke(questName);
    }
}
