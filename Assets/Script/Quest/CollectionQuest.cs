using UnityEngine;

public class CollectionQuest : QuestStep
{
    [SerializeField]
    public Item item;
    [SerializeField]
    private int current = 0;
    [SerializeField]
    private int required = 5;


    private void OnEnable()
    {
        InteractionManager.OnItemCollected += ItemCollected;
    }

    private void OnDisable()
    {
        InteractionManager.OnItemCollected -= ItemCollected;
    }

    private void ItemCollected(Item item)
    {
        Debug.Log("Collecting Item");
        if (this.item.data.itemName == item.data.itemName)
        {
            Debug.Log("Item Match!!!");
            if (current < required)
            {
                current++;
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
        string status = "Collected " + current + " / " + required + " " + item.data.itemName;
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.current = System.Int32.Parse(state);
        UpdateState();
    }
}
