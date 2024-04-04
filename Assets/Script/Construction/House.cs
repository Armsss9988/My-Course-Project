using UnityEngine;

public class House : MonoBehaviour, IDamageable
{
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float currentHealth;
    Animator animator;
    public float timeRespawn = 10f;
    float currentTimeRespawn;
    bool isDestroyed = false;
    Spawner[] spawners;
    GameObject burningEffect;
    bool isDamagebale = true;
    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        currentTimeRespawn = timeRespawn;
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
        GetComponent<Rigidbody2D>().AddForce(direction * amount, ForceMode2D.Impulse);
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
        if (isDestroyed)
        {
            currentTimeRespawn -= Time.deltaTime;
            if (currentTimeRespawn < 0)
            {
                this.currentHealth = maxHealth / 2;
                isDestroyed = false;
                animator.SetBool("Destroyed", false);
                currentTimeRespawn = timeRespawn;
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
}
