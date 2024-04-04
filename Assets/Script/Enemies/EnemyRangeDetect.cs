using UnityEngine;

public class EnemyRangeDetect : MonoBehaviour
{
    Enemy enemy;
    bool inAttackRange = false;
    bool detectCheck = false;
    private Collider2D[] objectsChecking;
    private float targetDistance;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemy.target != null)
        {

            Debug.Log("checking range");
            CheckTargetRanges();
            if (enemy.target.TryGetComponent<IDamageable>(out var damageable) && !damageable.IsDamageable())
            {
                enemy.target = null;
            }
        }
        else
        {
            Debug.Log("objectsChecking");
            CheckForNewTarget();
        }
    }
    public bool IsInAttackRange()
    {
        return inAttackRange;
    }
    public bool IsInDetectRange()
    {
        return detectCheck;
    }


    private void CheckTargetRanges()
    {
        targetDistance = Vector3.Distance(transform.position, enemy.target.transform.position);
        bool isWithinDetectRange = targetDistance <= enemy.detectRange;
        bool isWithinAttackZone = targetDistance > enemy.minAttackZone && targetDistance < enemy.maxAttackZone;
        inAttackRange = isWithinAttackZone;
        detectCheck = isWithinDetectRange;

        if (!isWithinDetectRange)
        {
            enemy.target = null;  // Reset target for re-detection
        }
    }
    public void CheckForNewTarget()
    {
        objectsChecking = Physics2D.OverlapCircleAll(this.transform.position, enemy.checkingRange);
        Collider2D nearestObject = GetNearestObjectWithTag(objectsChecking);

        if (nearestObject != null)
        {
            enemy.target = nearestObject.gameObject;
        }
    }
    private Collider2D GetNearestObjectWithTag(Collider2D[] objects)
    {
        Collider2D nearestObject = null;
        float minDistance = Mathf.Infinity;

        // Tính toán khoảng cách đến từng Object
        foreach (Collider2D obj in objects)
        {
            if (this.gameObject.IsTargetThisObject(obj.gameObject) && obj.TryGetComponent<IDamageable>(out var damageable) && damageable.IsDamageable())
            {
                float distance = Vector3.Distance(this.transform.position, obj.transform.position);

                // Cập nhật Object gần nhất
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestObject = obj;
                }
            }
        }

        return nearestObject;
    }
}
