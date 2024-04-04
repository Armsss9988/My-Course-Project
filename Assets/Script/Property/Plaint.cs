using UnityEngine;

public class Plaint : MonoBehaviour, IDamageable
{
    Animator animator;
    public float maxHealth = 50f;
    public float currentHealth = 50f;
    public float fruitTimer = 30f;
    public float currentFruitTime;
    public float growthTimer = 30f;
    public float currentGrowthTime;
    public ObjectState currentState = ObjectState.Regrowth;
    public GameObject fruit;
    bool isDamagebale = true;
    public enum ObjectState
    {
        Growth,
        Regrowth,
        Fruit
    }
    private void Awake()
    {
        currentGrowthTime = growthTimer;
        currentFruitTime = fruitTimer;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
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
        if (isDamagebale)
        {
            if (amount < 0 && (currentState != ObjectState.Regrowth))
            {
                animator.SetTrigger("Being Hit");


            }
            this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Regrowth");
            currentState = ObjectState.Regrowth;
            isDamagebale = false;
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
    public void Respawn()
    {
        if (currentState == ObjectState.Regrowth)
        {
            currentGrowthTime -= Time.deltaTime;
            if (currentGrowthTime < 0)
            {
                isDamagebale = true;
                this.currentHealth = maxHealth / 2;
                currentState = ObjectState.Growth;
                animator.SetTrigger("Growth");
                currentGrowthTime = growthTimer;
            }
        }
        if (currentState == ObjectState.Growth)
        {
            currentFruitTime -= Time.deltaTime;
            if (currentFruitTime < 0)
            {
                isDamagebale = true;
                this.currentHealth = maxHealth;
                currentState = ObjectState.Fruit;
                animator.SetTrigger("Fruit");
                currentFruitTime = fruitTimer;
            }
        }
    }
    public void Harvert()
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = new(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        GameObject droppedItem = Instantiate(fruit, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.layer = this.gameObject.layer;
        droppedItem.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponent<SpriteRenderer>().sortingLayerName;
    }

}
