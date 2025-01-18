using UnityEngine;
[System.Serializable]
public class Tree : MonoBehaviour, IDamageable
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
    [SerializeField]
    float timeRespawn = 5f;
    float currentTimeRespawn;
    bool isDestroyed = false;
    public bool isDamagebale = true;

    public string ID => id;
    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        currentTimeRespawn = timeRespawn;
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
        if (amount < 0)
        {
            animator.SetTrigger("Being Hit");
        }

        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("Destroy", true);
            isDestroyed = true;
        }
    }
    public void Respawn()
    {
        if (isDestroyed)
        {
            isDamagebale = false;
            currentTimeRespawn -= Time.deltaTime;
            if (currentTimeRespawn < 0)
            {
                this.currentHealth = maxHealth;
                isDestroyed = false;
                isDamagebale = true;
                animator.SetBool("Destroy", false);
                currentTimeRespawn = timeRespawn;
            }
        }
    }
    public void SourceAttackSound(AudioClip sourceAttackSound)
    {
        GetComponent<AudioSource>().PlayOneShot(sourceAttackSound);
    }

    public void Force(Vector2 direction, int amount)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * amount, ForceMode2D.Impulse);
    }
    public void LoadData(TreeData treeData)
    {
        this.currentHealth = treeData.currentHealth;
        this.currentTimeRespawn = treeData.currentTimeRespawn;
        ChangeHealth(0f);
    }
    public TreeData SaveData()
    {
        return new TreeData(this.id, this.currentHealth, this.currentTimeRespawn);
    }
    private void OnEnable()
    {
        WorldManager.instance.AddTree(this);
    }
    private void OnDisable()
    {
        WorldManager.instance.RemoveTree(this);
    }
}
