using UnityEngine;

public class CharacterDialog : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    [SerializeField] float talkDistance = 2;
    public float targetColliderOffset = 0.5f;
    bool inConversation;


    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
    }


    void Interact()
    {
        Debug.Log("Interacting");
        if (inConversation)
        {
            DialogueBoxController.instance.SkipLine();
        }
        else
        {
            Debug.Log("Finding!");
            RaycastHit2D hit = Physics2D.Raycast((Vector2)this.transform.position + (characterAnimation.lookDirection).normalized * targetColliderOffset,
                characterAnimation.lookDirection,
                talkDistance);
            Debug.DrawRay((Vector2)this.transform.position + (characterAnimation.lookDirection).normalized * targetColliderOffset,
                characterAnimation.lookDirection,
                Color.red, talkDistance);
            if (hit.collider != null)
            {
                Debug.Log("Finded: " + hit.collider.name);
                if (hit.collider.gameObject.TryGetComponent(out Actor actor))
                {
                    if (hit.collider.gameObject.TryGetComponent(out NPCDialog dialog))
                    {
                        Debug.Log("Finded NPC");
                        DialogueBoxController.instance.StartDialogue(actor);
                    }
                }
            }
        }
    }

    void JoinConversation()
    {
        Debug.Log("Joined");
        inConversation = true;
    }

    void LeaveConversation()
    {
        Debug.Log("Out Conversation");
        inConversation = false;
    }

    private void OnEnable()
    {
        Debug.Log("Char dialog Enable");
        InteractionManager.OnInteraction += Interact;
        UIManager.OnOpenDialog += JoinConversation;
        UIManager.OnCloseDialog += LeaveConversation;
    }

    private void OnDisable()
    {
        Debug.Log("Char dialog Disable");
        InteractionManager.OnInteraction -= Interact;
        UIManager.OnOpenDialog -= JoinConversation;
        UIManager.OnCloseDialog -= LeaveConversation;
    }
}
