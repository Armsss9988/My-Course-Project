using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public Inventory inventory;
    public int indexSlotArrow;
    CharacterAction characterAction;
    CharacterSound characterSound;
    public float currentHealth;
    public float maxHealth = 100f;
    public float timeInvincible = 1.0f;
    public float damage;
    public bool isInvincible;
    float invincibleTimer;
    bool isDamagebale = true;

    private void Awake()
    {
        inventory = new Inventory(30);
        characterSound = GetComponent<CharacterSound>();
        characterAction = GetComponent<CharacterAction>();
        currentHealth = maxHealth;

    }
    void Start()
    {
        PlayerHealthText.instance.SetText(currentHealth, maxHealth);
    }
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
                characterAction.BeingHit();
            }
        }
        Debug.Log("Hit");
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            isDamagebale = false;
        }
        Debug.Log(currentHealth);

        PlayerHealthBar.instance.SetValue(currentHealth / maxHealth);
        PlayerHealthText.instance.SetText(currentHealth, maxHealth);
    }
    public bool IsArrowInInventory()
    {
        foreach (Inventory.Slot slot in inventory.slots)
        {
            Item item = GameManager.instance.itemManager.GetItemByName(slot.itemName);
            if (item != null)
            {
                if (item.data is ArrowData arrow)
                {
                    Debug.Log("Have Arrow");
                    indexSlotArrow = inventory.slots.IndexOf(slot);
                    characterAction.SetArrow(item);
                    return true;
                }
            }
        }
        Debug.Log("Dont Have Arrow");
        return false;
    }
    public void RemoveArrow(int num)
    {
        inventory.Remove(indexSlotArrow, num);
        Toolbar_UI.instance.Refresh();
        Inventory_UI.instance.Refresh();
    }

    public void SourceAttackSound(AudioClip sourceAttackSound)
    {
        characterSound.PlaySound(sourceAttackSound);
    }

    public void Force(Vector2 direction, int amount)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * amount, ForceMode2D.Impulse);
    }
}
