using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public Inventory inventory;
    public int indexSlotArrow;
    CharacterAction characterAction;
    CharacterSound characterSound;
    CharacterEquipment characterEquipment;

    public float currentHealth;
    public float baseHealth = 100f;
    public float bonusHealth = 0f;
    public float maxHealth;

    public float baseSpeed = 1f;
    public float bonusSpeed = 1f;
    public float maxSpeed;

    public float baseAttackSpeed = 1f;
    public float bonusAttackSpeed = 1f;
    public float maxAttackSpeed;

    public float timeInvincible = 1.0f;
    public float damage;
    public bool isInvincible;
    float invincibleTimer;
    bool isDamagebale = true;

    private void Awake()
    {
        inventory = new Inventory(36);
        characterSound = GetComponent<CharacterSound>();
        characterAction = GetComponent<CharacterAction>();
        characterEquipment = GetComponent<CharacterEquipment>();
        maxHealth = baseHealth + bonusHealth;
        currentHealth = maxHealth;
        maxSpeed = baseSpeed * bonusSpeed;
        maxAttackSpeed = baseAttackSpeed * bonusAttackSpeed;

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
    public void HandleEquipmentBonus()
    {
        bonusHealth = 0f;
        bonusAttackSpeed = 1f;
        bonusSpeed = 1f;
        if (characterEquipment.torso != null && characterEquipment.torso.data is ArmorData torso)
        {
            bonusHealth += torso.healthBonus;
            bonusSpeed += torso.speedBonus;
            bonusAttackSpeed += torso.attackspeedBonus;

        }
        if (characterEquipment.pant != null && characterEquipment.pant.data is ArmorData pant)
        {
            bonusHealth += pant.healthBonus;
            bonusSpeed += pant.speedBonus;
            bonusAttackSpeed += pant.attackspeedBonus;
        }
        if (characterEquipment.shoes != null && characterEquipment.shoes.data is ArmorData shoes)
        {
            bonusHealth += shoes.healthBonus;
            bonusSpeed += shoes.speedBonus;
            bonusAttackSpeed += shoes.attackspeedBonus;
        }
        if (characterEquipment.gloves != null && characterEquipment.gloves.data is ArmorData gloves)
        {
            bonusHealth += gloves.healthBonus;
            bonusSpeed += gloves.speedBonus;
            bonusAttackSpeed += gloves.attackspeedBonus;
        }
        maxHealth = baseHealth + bonusHealth;
        maxSpeed = baseSpeed * bonusSpeed;
        maxAttackSpeed = baseAttackSpeed * bonusAttackSpeed;
        ChangeHealth(0f);
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
            if (slot.type == Inventory.Slot.Type.Arrow)
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
