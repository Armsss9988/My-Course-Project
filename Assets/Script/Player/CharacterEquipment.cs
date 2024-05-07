using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterEquipment : MonoBehaviour
{
    CharacterAction characterAction;
    Character character;
    GameObject characterTorso;
    GameObject characterPant;
    GameObject characterShoes;
    GameObject characterGloves;
    public Item? torso;
    public Item? pant;
    public Item? shoes;
    public Item? gloves;
    public Item? arrow;
    public Item? shield;
    void Start()
    {
        character = GetComponent<Character>();
        characterAction = GetComponent<CharacterAction>();
        characterTorso = this.transform.Find("Torso").gameObject;
        characterPant = this.transform.Find("Pant").gameObject;
        characterShoes = this.transform.Find("Shoes").gameObject;
        characterGloves = this.transform.Find("Gloves").gameObject;
    }

    public void ChangeTorso(Item torsoChange)
    {
        torso = torsoChange;
        ChangeArmorAsset(torso, characterTorso);
        character.HandleEquipmentBonus();
    }
    public void ChangePant(Item pantChange)
    {
        pant = pantChange;
        ChangeArmorAsset(pant, characterPant);
        character.HandleEquipmentBonus();
    }
    public void ChangeShoes(Item shoesChange)
    {
        shoes = shoesChange;
        ChangeArmorAsset(shoes, characterShoes);
        character.HandleEquipmentBonus();
    }
    public void ChangeGloves(Item glovesChange)
    {
        gloves = glovesChange;
        ChangeArmorAsset(gloves, characterGloves);
        character.HandleEquipmentBonus();
    }
    public void ChangeShield(Item shieldChange)
    {
        shield = shieldChange;
    }
    public void ChangeArrow(Item arrowChange)
    {
        arrow = arrowChange;
        if (arrow != null)
        {
            characterAction.SetArrow(arrow);
        }
    }
    void ChangeArmorAsset(Item item, GameObject gameObject)
    {
        if (item != null)
        {
            if (gameObject.TryGetComponent<SpriteLibrary>(out var spriteLibrary) && item.data is ArmorData armorData)
            {
                spriteLibrary.spriteLibraryAsset = armorData.libraryAsset;
                gameObject.GetComponent<SpriteRenderer>().color = armorData.color;
            }
        }
        else
        {
            if (gameObject.TryGetComponent<SpriteLibrary>(out var spriteLibrary))
            {
                spriteLibrary.spriteLibraryAsset = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

    }


}
