using System.Collections.Generic;
using UnityEngine;
public class AIController : MonoBehaviour
{
    [HideInInspector] public NPCData npcData;
    public NpcEvent npcEvent = new();
    public CacheComponent cacheComponent;
    public Dictionary<Condition, bool> conditions = new();
    [SerializeField] protected BehaviourTreeSO BehaviourTreeSO;
    public ActionNode currentAcction;
    public bool IsMoving { get; set; }
    public float Speed { get; set; }
    [SerializeField] public Vector2 MovingTarget { get; set; }
    public Vector2 FirstPosition { get; set; }
    [HideInInspector] public Animator animator;
    public GameObject Target;


    public void SetMovingTarget(Vector2 newTarget)
    {
        IsMoving = true;
        MovingTarget = newTarget;
    }
    public void Awake()
    {
        npcData = GetComponent<NPCData>();
        FirstPosition = transform.position;
        MovingTarget = transform.position;
        Speed = npcData.movementSpeed;
        cacheComponent = new CacheComponent(this.gameObject);
        animator = GetComponent<Animator>();
        conditions.Add(Condition.IsTargetInAttackRange, false);
        conditions.Add(Condition.IsTargetInAttackZone, false);
        conditions.Add(Condition.IsTargetInDetectRange, false);
    }
    private void OnEnable()
    {
        npcEvent.OnDetectTarget += OnDetectTarget;
        npcEvent.OnUndetectTarget += OnUndetectTarget;
        npcEvent.OnTargetOutRange += OnTargetOutRange;
        npcEvent.OnTargetInRange += OnTargetInRange;

    }
    private void OnDisable()
    {
        npcEvent.OnDetectTarget -= OnDetectTarget;
        npcEvent.OnUndetectTarget -= OnUndetectTarget;
        npcEvent.OnTargetOutRange -= OnTargetOutRange;
        npcEvent.OnTargetInRange -= OnTargetInRange;

    }
    public T GetCachedComponent<T>() where T : Component
    {
        return cacheComponent.GetComponent<T>();
    }
    public Y GetCacheValue<Y>(string name)
    {
        return cacheComponent.GetValue<Y>(name);
    }
    public void SetCacheValue<Y>(string name, Y value)
    {
        cacheComponent.SetValue(name, value);
    }
    void OnDetectTarget(GameObject target)
    {
        Debug.Log("OnDetectTarget " + target.name);
        this.Target = target;
        conditions[Condition.IsTargetInDetectRange] = true;

    }
    void OnUndetectTarget(GameObject target)
    {
        Debug.Log("OnUndetectTarget " + target.name);
        this.Target = null;
        conditions[Condition.IsTargetInDetectRange] = false;

    }
    void OnTargetInRange(GameObject target)
    {
        Debug.Log("OnTargetInRange " + target.name);
        conditions[Condition.IsTargetInAttackRange] = true;

    }
    void OnTargetOutRange(GameObject target)
    {
        Debug.Log("OnTargetOutRange " + target.name);
        conditions[Condition.IsTargetInAttackRange] = false;

    }


    public void Update()
    {
        if (BehaviourTreeSO != null)
        {
            BehaviourTreeSO.Execute(this);
        }
    }

}


