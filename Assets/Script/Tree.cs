using UnityEngine;
[System.Serializable]
public class Tree : MonoBehaviour, IDamageable
{
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float currentHealth;
    Animator animator;
    float timeRespawn = 5f;
    float currentTimeRespawn;
    bool isDestroyed = false;
    public bool isDamagebale = true;
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
        Debug.Log("Tree change health: " + amount);
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
}
