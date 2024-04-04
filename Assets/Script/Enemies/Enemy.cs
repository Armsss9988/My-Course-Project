using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IAttackable
{
    public HealthBar healthBar;
    public HealthText healthText;
    bool isDamagebale = true;
    EnemyAction enemyAction;
    public float currentHealth;
    public float maxHealth;
    public float damage;
    public float checkingRange;
    public float detectRange;
    public float minAttackZone;
    public float maxAttackZone;
    public float speed;
    public float attackSpeed;
    public AudioClip attackSound;
    public AudioClip weaponAttackSound;
    public AudioClip weaponHitSound;
    public AudioClip beingHitSound;
    public AudioClip deadSound;
    public GameObject bullet;
    public float timeInvincible = 1.0f;
    public bool isInvincible;
    float invincibleTimer;
    [HideInInspector]
    public GameObject target;
    void Awake()
    {
        enemyAction = GetComponent<EnemyAction>();
    }
    private void Start()
    {
        ChangeHealth(0f);
    }


    // Update is called once per frame
    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
        }
        if (invincibleTimer < 0f)
        {
            isInvincible = false;
        }

    }
    public bool IsDamageable()
    {
        return isDamagebale;
    }
    public void ChangeHealth(float amount)
    {
        if (amount < 0f)
        {
            if (isInvincible)
            {
                return;
            }
            else
            {
                isInvincible = true;
                invincibleTimer = timeInvincible;
                this.enemyAction.BeingHit();
            }
        }

        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        if (currentHealth <= 0)
        {
            enemyAction.Dead();
            isDamagebale = false;
        }
        if (amount != 0f)
        {
            this.healthBar.SetValue(currentHealth / maxHealth);
        }
        this.healthText.SetText(currentHealth, maxHealth);
    }
    public void SourceAttackSound(AudioClip sourceAttackSound)
    {
        GetComponent<EnemySound>().PlaySound(sourceAttackSound);
    }

    public void Force(Vector2 direction, int amount)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * amount, ForceMode2D.Impulse);
    }

    public void SetTarget(GameObject gameObject)
    {
        if (gameObject != null)
        {
            if (this.gameObject.IsTargetThisObject(gameObject))
            {
                this.target = gameObject;
            }
        }
    }
}
