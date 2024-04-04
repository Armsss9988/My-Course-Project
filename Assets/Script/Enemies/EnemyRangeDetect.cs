using UnityEngine;

public class EnemyRangeDetect : MonoBehaviour
{
    Enemy enemy;
    bool isTargetInCheckingRange = false;
    bool inAttackRange = false;
    bool detectCheck = false;
    private Collider2D[] objectsChecking;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.target != null)
        {
            if (enemy.target.TryGetComponent<IDamageable>(out var damageable) && !damageable.IsDamageable())
            {
                enemy.target = null;
            }
            else
            {
                if (isTargetInCheckingRange)
                {
                    DetectPlayer();
                    CheckRangeAttack();
                }
            }
        }
        else
        {
            CheckIsTargetInRange();
        }

    }
    public bool IsInAttackRange()
    {
        return inAttackRange;
    }
    public bool IsInDetectRange() { return detectCheck; }


    public void DetectPlayer()
    {

        if ((enemy.target.transform.position - this.transform.position).magnitude <= Mathf.Abs(enemy.detectRange))
        {
            detectCheck = true;
        }
        else if ((enemy.target.transform.position - transform.position).magnitude >= Mathf.Abs(enemy.checkingRange))
        {
            detectCheck = false;
            enemy.target = null;
            isTargetInCheckingRange = false;
        }
    }
    public void CheckRangeAttack()
    {
        if (enemy.target != null
            && (enemy.target.gameObject.layer == this.gameObject.layer)
            && (enemy.target.transform.position - transform.position).magnitude > Mathf.Abs(enemy.minAttackZone)
            && (enemy.target.transform.position - transform.position).magnitude < Mathf.Abs(enemy.maxAttackZone))
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }
    }
    public void CheckIsTargetInRange()
    {
        objectsChecking = Physics2D.OverlapCircleAll(this.transform.position, enemy.checkingRange);
        Collider2D nearestObject = GetNearestObjectWithTag(objectsChecking);

        if (nearestObject != null)
        {
            enemy.target = nearestObject.gameObject;
            isTargetInCheckingRange = true;
        }
        else
        {
            isTargetInCheckingRange = false;
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