using UnityEngine;

public class HandleInput : MonoBehaviour
{
    CharacterAction characterAction;
    CharacterAnimation characterAnimation;
    CharacterMovement characterMovement;
    float horizontal;
    float vertical;
    void Start()
    {
        characterAction = GetComponent<CharacterAction>();
        characterAnimation = GetComponent<CharacterAnimation>();
        characterMovement = GetComponent<CharacterMovement>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            QuestLogUI.instance.ToggleQuestLog();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory_UI.instance.ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuUI.instance.ToggleMenu();
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        characterMovement.MovementPosition(horizontal, vertical);
        characterAnimation.MovementAnimation(horizontal, vertical);
    }
}
