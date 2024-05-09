using BayatGames.SaveGameFree;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public static Character instance;
    public Inventory inventory;
    public int indexSlotArrow;
    CharacterAction characterAction;
    CharacterSound characterSound;
    CharacterEquipment characterEquipment;
    CharacterDataSO characterDataSO;
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
    Vector3 startPoint;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        startPoint = this.transform.position;
        indexSlotArrow = 35;
        characterDataSO = Resources.Load<CharacterDataSO>("PlayerData");
        characterSound = GetComponent<CharacterSound>();
        characterAction = GetComponent<CharacterAction>();
        characterEquipment = GetComponent<CharacterEquipment>();
        inventory = new Inventory(36);

    }
    void Start()
    {
        LoadPlayerDefault();
        currentHealth = maxHealth;
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
    private void OnEnable()
    {
        /*GameManager.OnNewGame += ResetGame;*/
        GameManager.OnSaveGame += OnSaveGame;
        GameManager.OnLoadGame += OnLoadGame;
    }
    private void OnDisable()
    {
        /* GameManager.OnNewGame -= ResetGame;*/
        GameManager.OnSaveGame -= OnSaveGame;
        GameManager.OnLoadGame -= OnLoadGame;
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
        RefreshPlayerData();
    }
    void LoadPlayerDefault()
    {
        baseHealth = characterDataSO.baseHealth;
        bonusHealth = characterDataSO.bonusHealth;
        baseAttackSpeed = characterDataSO.baseAttackSpeed;
        bonusAttackSpeed = characterDataSO.bonusAttackSpeed;
        baseSpeed = characterDataSO.baseSpeed;
        bonusSpeed = characterDataSO.bonusSpeed;
        RefreshPlayerData();
    }
    void RefreshPlayerData()
    {
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
        return inventory.slots[indexSlotArrow].itemName != "";
    }
    public void RemoveArrow(int num)
    {
        inventory.Remove(indexSlotArrow, num);
        Toolbar_UI.instance.Refresh();
        Inventory_UI.instance.Refresh();
    }
    public int GetItemCount(Item item)
    {
        int itemQuantity = 0;
        foreach (Inventory.Slot slot in inventory.slots)
        {
            if (slot.itemName == item.data.itemName)
            {
                itemQuantity += slot.count;
            }
        }
        return itemQuantity;
    }

    public void SourceAttackSound(AudioClip sourceAttackSound)
    {
        characterSound.PlaySound(sourceAttackSound);
    }

    public void Force(Vector2 direction, int amount)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * amount, ForceMode2D.Impulse);
    }
    public void ResetGame()
    {
        this.transform.position = startPoint;
        this.currentHealth = baseHealth;
    }
    public void OnSaveGame()
    {
        Debug.Log("saving char data: ");
        SaveData saveData = GameManager.instance.saveData;

        saveData.characterData = new CharacterData(this.currentHealth, this.transform.position);
        saveData.inventoryData = new InventoryData(this.inventory.slots);
        saveData.equipmentData = new EquipmentData
        (
            characterEquipment.torso ? characterEquipment.torso.data.itemName : null,
            characterEquipment.pant ? characterEquipment.pant.data.itemName : null,
            characterEquipment.shoes ? characterEquipment.shoes.data.itemName : null,
            characterEquipment.gloves ? characterEquipment.gloves.data.itemName : null,
            characterEquipment.shield ? characterEquipment.shield.data.itemName : null,
            characterEquipment.arrow ? characterEquipment.arrow.data.itemName : null
        );

    }
    public void OnLoadGame()
    {
        if (SaveGame.Exists("saveData"))
        {

            SaveData saveData = GameManager.instance.saveData;

            this.currentHealth = saveData.characterData.health;
            this.transform.position = saveData.characterData.position;
            this.inventory.slots = saveData.inventoryData.slots;

            foreach (Inventory.Slot slot in inventory.slots)
            {
                if (slot.itemName != null)
                {
                    Item item = ItemManager.instance.GetItemByName(slot.itemName);
                    if (item != null)
                    {
                        slot.icon = item.data.icon;
                    }

                }
            }
            characterEquipment.ChangeTorso(ItemManager.instance.GetItemByName(saveData.equipmentData.torso));
            characterEquipment.ChangePant(ItemManager.instance.GetItemByName(saveData.equipmentData.pant));
            characterEquipment.ChangeShoes(ItemManager.instance.GetItemByName(saveData.equipmentData.shoes));
            characterEquipment.ChangeGloves(ItemManager.instance.GetItemByName(saveData.equipmentData.gloves));
            characterEquipment.ChangeShield(ItemManager.instance.GetItemByName(saveData.equipmentData.shield));
            characterEquipment.ChangeArrow(ItemManager.instance.GetItemByName(saveData.equipmentData.arrow));
            Toolbar_UI.instance.Refresh();
            Inventory_UI.instance.Refresh();
        }

    }



}
