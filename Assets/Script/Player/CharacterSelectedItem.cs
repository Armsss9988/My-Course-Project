using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterSelectedItem : MonoBehaviour
{
    Item selectedItem, previousItem = null;
    GameObject hand;
    Character character;

    void Start()
    {
        character = GetComponent<Character>();
        hand = this.transform.Find("Hand").gameObject;
    }
    public Item GetSelectedItem()
    {
        return selectedItem;
    }

    public void HandleItemChange(Item item)
    {
        if (previousItem != null)
        {
            if (previousItem.data is WeaponData weaponData)
            {
                character.damage -= weaponData.damage;
                character.GetComponent<TargetTags>().damageableTypeTags.RemoveAll(obj => weaponData.attackTypeTags.Contains(obj)); ;
            }
        }
        if (item == null)
        {
            selectedItem = null;
            hand.GetComponent<SpriteLibrary>().spriteLibraryAsset = null;
            hand.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            selectedItem = item;
            if (selectedItem.data is WeaponData weaponData)
            {
                hand.transform.localPosition = new Vector3(0, -0.01f);
                hand.GetComponent<SpriteLibrary>().spriteLibraryAsset = selectedItem.GetComponent<SpriteLibrary>().spriteLibraryAsset;
                character.GetComponent<TargetTags>().damageableTypeTags = weaponData.attackTypeTags;
                character.damage += weaponData.damage;
            }
            else
            {
                hand.transform.localPosition = new Vector3(0, 0.6f);
                character.GetComponent<TargetTags>().attackTypeTags = null;
                hand.GetComponent<SpriteLibrary>().spriteLibraryAsset = null;
                hand.GetComponent<SpriteRenderer>().sprite = selectedItem.GetComponent<SpriteRenderer>().sprite;
            }
        }
        previousItem = selectedItem;

    }



}
