using UnityEngine;


[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    public string question;
    [TextArea]
    public string[] dialogue;
    [SerializeField]
    public DialogueAsset[] Choices;


}

