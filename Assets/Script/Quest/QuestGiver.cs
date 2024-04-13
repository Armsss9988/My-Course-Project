using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> listQuest;

    private void Awake()
    {

    }





    bool IsMeetRequirement(List<Quest> requiredQuests, List<Quest> availQuest)
    {

        HashSet<Quest> set = new HashSet<Quest>(requiredQuests);
        set.IntersectWith(availQuest);
        return set.Count == 0;
    }




}
