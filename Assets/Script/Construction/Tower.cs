using UnityEngine;
[System.Serializable]
public class Tower : MonoBehaviour, IDamageable
{
    [SerializeField] public string id;
    [ContextMenu("Generate id")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float currentHealth;
    Animator animator;
    public float timeConstruct = 20f;
    float currentTimeConstruct;
    public float timeRespawn = 10f;
    float currentTimeRespawn;
    bool isDestroyed = false;
    bool isInConstruct = false;
    Spawner[] spawners;
    GameObject burningEffect;
    bool isDamagebale = true;


    public string ID => id;
    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        currentTimeRespawn = timeRespawn;
        currentTimeConstruct = timeConstruct;
        spawners = GetComponentsInChildren<Spawner>();
        burningEffect = this.transform.Find("Burning").gameObject;
    }
    private void Update()
    {
        Respawn();
    }
    public bool IsDamageable()
    {
        return isDamagebale;
    }
    public void ChangeHealth(float amount)
    {
        if (amount < 0 && !isDestroyed)
        {
            animator.SetTrigger("Being Hit");
            if (burningEffect != null)
            {
                burningEffect.SetActive(true);
                SetBurningAmount();
            }

        }

        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("Destroyed", true);
            animator.SetBool("InConstruct", false);
            isInConstruct = false;
            isDestroyed = true;
            isDamagebale = false;
            if (burningEffect != null)
            {
                burningEffect.SetActive(false);
            }
            StopSpawner();
        }
    }
    public void SourceAttackSound(AudioClip sourceAttackSound)
    {
        GetComponent<AudioSource>().PlayOneShot(sourceAttackSound);
    }

    public void Force(Vector2 direction, int amount)
    {
    }
    void SetBurningAmount()
    {
        if (burningEffect != null)
        {
            for (int i = 0; i < burningEffect.transform.childCount; i++)
            {
                burningEffect.transform.GetChild(i).localScale = Vector3.one * (maxHealth / currentHealth) * 0.3f;
            }
        }
    }
    public void Respawn()
    {
        if (isDestroyed && !isInConstruct)
        {
            currentTimeRespawn -= Time.deltaTime;
            if (currentTimeRespawn < 0)
            {
                this.currentHealth = maxHealth / 2;
                isInConstruct = true;
                isDestroyed = false;
                animator.SetBool("Destroyed", false);
                animator.SetBool("InConstruct", true);
                currentTimeRespawn = timeRespawn;
            }
        }
        if (!isDestroyed && isInConstruct)
        {
            currentTimeConstruct -= Time.deltaTime;
            if (currentTimeConstruct < 0)
            {
                this.currentHealth = maxHealth;
                isInConstruct = false;
                animator.SetBool("InConstruct", false);
                currentTimeConstruct = timeConstruct;
                StartSpawner();
            }
        }
    }
    public void StopSpawner()
    {
        if (spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                spawner.isSpawning = false;
            }
        }

    }
    public void StartSpawner()
    {
        if (spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                spawner.isSpawning = true;
            }
        }
    }
    public void LoadData(TowerData towerData)
    {
        this.currentHealth = towerData.currentHealth;
        this.currentTimeRespawn = towerData.currentTimeRespawn;
        this.currentTimeConstruct = towerData.currentTimeConstruct;
        this.currentTimeRespawn = towerData.currentTimeRespawn;
        this.isDestroyed = towerData.isDestroyed;
        this.isInConstruct = towerData.isInConstruct;
        this.isDamagebale = towerData.isDamagebale;
        ChangeHealth(0f);
    }
    public TowerData SaveData()
    {
        return new TowerData(this.id, this.currentHealth, this.currentTimeConstruct, this.currentTimeRespawn, this.isDestroyed, this.isInConstruct, this.isDamagebale);
    }
    private void OnEnable()
    {
        WorldManager.instance.AddTower(this);
    }
    private void OnDisable()
    {
        WorldManager.instance.RemoveTower(this);
    }
}
