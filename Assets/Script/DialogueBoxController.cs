using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxController : MonoBehaviour
{
    public static DialogueBoxController instance;
    bool isOpen = false;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] CanvasGroup dialogueBox;
    bool skipLineTriggered;
    public GameObject ChoiceUIPreb;
    public GameObject CloseBoxPreb;
    public GameObject ChoiceContent;
    public GameObject CurrentActiveStepContent;
    public Actor currentActor;
    public DialogState state;
    public AudioSource audioSource;
    public AudioClip letterSound;
    public enum DialogState { Begin, Quest, Talking }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void CreateQuestChoice()
    {

        List<Quest> actorQuests = QuestManager.instance.FindQuestsWithSameActor(QuestManager.instance.ListQuests, currentActor);

        if (actorQuests != null && actorQuests.Count > 0)
        {
            foreach (Quest actorQuest in actorQuests)
            {
                Debug.Log("Actor quest: " + actorQuest);
                if (actorQuest != null && actorQuest.questStatus == Quest.QuestStatus.CanStart)
                {
                    ChoiceUI choiceUI = ChoiceUIPreb.GetComponent<ChoiceUI>();
                    choiceUI.SetInfo(actorQuest, currentActor, ChoiceUI.ChoiceState.StartQuest);
                    GameObject.Instantiate(ChoiceUIPreb, ChoiceContent.transform);
                }
            }

        }
        QuestStep[] arrayCurrentStep = CurrentActiveStepContent.GetComponentsInChildren<QuestStep>();
        foreach (QuestStep step in arrayCurrentStep)
        {
            if (step is TalkToNPCQuest talkToNPCQuest)
            {
                if (talkToNPCQuest.Actor.ActorName == currentActor.ActorName)
                {
                    ChoiceUI choiceUI = ChoiceUIPreb.GetComponent<ChoiceUI>();
                    choiceUI.SetInfo(talkToNPCQuest.quest, currentActor, ChoiceUI.ChoiceState.FinishStep, talkToNPCQuest.StepIndex);

                    GameObject.Instantiate(ChoiceUIPreb, ChoiceContent.transform);
                }
            }
        }
        AddCloseChoice();
    }

    public void OpenDialogBox()
    {
        isOpen = true;
        dialogueBox.gameObject.SetActive(true);
    }
    public void CloseDialogBox()
    {
        isOpen = false;
        dialogueBox.gameObject.SetActive(false);
    }
    public void StartDialogue(Actor actor)
    {
        StopAllCoroutines();
        state = DialogState.Begin;
        currentActor = actor;
        UIManager.instance.OpenDialog();
        StartCoroutine(RunDialogue(currentActor.GetComponent<NPCDialog>().dialogueAsset));
    }
    public void ContinueNewDialog(DialogueAsset dialogueAsset)
    {
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogueAsset));
    }

    public IEnumerator RunDialogue(DialogueAsset dialogueAsset)
    {

        for (int i = ChoiceContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ChoiceContent.transform.GetChild(i).gameObject);
        }
        Debug.Log("Dialog: " + dialogueAsset.dialogue[0].ToString());
        yield return new WaitForEndOfFrame();
        skipLineTriggered = false;

        string[] dialogue = dialogueAsset.dialogue;
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text = "";
            foreach (var letter in dialogue[i])
            {
                audioSource.PlayOneShot(letterSound);
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.03f);
            }
            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }
        if (this.state == DialogState.Begin)
        {
            CreateQuestChoice();
        }
        else
        {
            AddCloseChoice();
        }
    }
    void CreateDialogChoice(Actor actor)
    {

    }
    void AddCloseChoice()
    {
        {
            GameObject closeButton = Instantiate(CloseBoxPreb, ChoiceContent.transform);
            closeButton.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.CloseDialog(); });

            // Optional: Wait for a frame to ensure component is ready
            StartCoroutine(WaitForButton(closeButton));
        }

        IEnumerator WaitForButton(GameObject buttonObject)
        {
            yield return new WaitForEndOfFrame();
            buttonObject.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.CloseDialog(); });
        }
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
    public void StopRunningDialog()
    {
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        UIManager.OnOpenDialog += OpenDialogBox;
        UIManager.OnCloseDialog += CloseDialogBox;
    }

    private void OnDisable()
    {
        UIManager.OnOpenDialog -= OpenDialogBox;
        UIManager.OnCloseDialog -= CloseDialogBox;
    }
}
