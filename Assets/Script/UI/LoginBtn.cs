using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoginBtn : MonoBehaviour
{
    [SerializeField] TMP_Text username;
    [SerializeField] TMP_Text password;


    public void AddTriggerListener(EventTrigger trigger, EventTriggerType eventType, Action action)
    {
        EventTrigger.Entry entry = new()
        {
            eventID = eventType
        };
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }


}
