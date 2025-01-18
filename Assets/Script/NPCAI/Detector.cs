using UnityEngine;
public class Detector : MonoBehaviour, ISense
{
    NPCData npcData;
    NpcEvent npcEvent;
    AIController controller;
    bool isTargetInAttackRange = false;
    private Collider2D[] objectsChecking;
    float distance;
    float checkTimer = 1.0f;
    float currentTime = 0;

    void Start()
    {
        controller = GetComponent<AIController>();
        npcEvent = controller.npcEvent;
        npcData = GetComponent<NPCData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentTime <= checkTimer)
        {
            currentTime += Time.deltaTime;
            return;
        }
        else currentTime = 0;
        DetectTarget();
        UndetectTarget();
        CheckRangeAttack();

    }

    public void UndetectTarget()
    {
        if (controller.Target)
        {
            Vector3 targetPosition = controller.Target.transform.position;
            Vector3 thisPosition = transform.position;
            distance = Vector3.Distance(targetPosition, thisPosition);
            if (distance >= npcData.checkingRange || (controller.Target.TryGetComponent<IDamageable>(out var damageable) && !damageable.IsDamageable()))

            {
                npcEvent.UndetectTarget(controller.Target);
            }
        }
    }
    public void CheckRangeAttack()
    {
        if (controller.Target == null || controller.Target.layer != this.gameObject.layer
            || distance <= npcData.minAttackZone
            || distance >= npcData.maxAttackZone)
        {
            npcEvent.TargetOutRange(controller.Target);
        }
        else
        {
            npcEvent.TargetInRange(controller.Target);
        }
    }
    public void DetectTarget()
    {
        if (!controller.Target)
        {
            objectsChecking = Physics2D.OverlapCircleAll(this.transform.position, npcData.checkingRange);
            Collider2D nearestObject = GetNearestObjectWithTag(objectsChecking);

            if (nearestObject != null)
            {
                npcEvent.DetectTarget(nearestObject.gameObject);
            }
        }

    }
    private Collider2D GetNearestObjectWithTag(Collider2D[] objects)
    {
        Collider2D nearestObject = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D obj in objects)
        {
            if (this.gameObject.IsTargetThisObject(obj.gameObject) && obj.TryGetComponent<IDamageable>(out var damageable) && damageable.IsDamageable())
            {
                float distance = Vector3.Distance(this.transform.position, obj.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestObject = obj;
                }
            }
        }
        return nearestObject;
    }

    public void EnableSense()
    {
        throw new System.NotImplementedException();
    }

    public void DisableSense()
    {
        throw new System.NotImplementedException();
    }

}