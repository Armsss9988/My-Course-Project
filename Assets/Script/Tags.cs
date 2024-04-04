using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public List<TypeTag> typeTags;
    public List<AttackTypeTag> attackTypeTags;
    public List<FactionTag> factionTags;
    public List<AttackTypeTag> damageableTypeTag;

    public bool HasTypeTag(TypeTag t)
    {
        return typeTags.Contains(t);
    }
    public bool HasTypeTag(string tagname)
    {
        return typeTags.Exists(t => t.Name.Equals(tagname, System.StringComparison.InvariantCultureIgnoreCase));
    }
    public bool ContainsAllTypeTags(List<TypeTag> checkingTypeTags)
    {
        return new HashSet<TypeTag>(typeTags).IsSupersetOf(new HashSet<TypeTag>(checkingTypeTags));
    }

    public bool HasAttackTypeTag(AttackTypeTag t)
    {
        return attackTypeTags.Contains(t);
    }
    public bool HasAttackTypeTag(string tagname)
    {
        return attackTypeTags.Exists(t => t.Name.Equals(tagname, System.StringComparison.InvariantCultureIgnoreCase));
    }
    public bool ContainsAllAttackTypeTags(List<AttackTypeTag> checkingTags)
    {
        return new HashSet<AttackTypeTag>(attackTypeTags).IsSupersetOf(new HashSet<AttackTypeTag>(checkingTags));
    }

    public bool HasDamageableTypeTag(AttackTypeTag t)
    {
        return damageableTypeTag.Contains(t);
    }
    public bool HasDamageableTypeTag(string tagname)
    {
        return damageableTypeTag.Exists(t => t.Name.Equals(tagname, System.StringComparison.InvariantCultureIgnoreCase));
    }
    public bool ContainsAllDamageableTypeTags(List<AttackTypeTag> checkingTags)
    {
        return new HashSet<AttackTypeTag>(damageableTypeTag).IsSupersetOf(new HashSet<AttackTypeTag>(checkingTags));
    }

    public bool HasFactionTag(FactionTag t)
    {
        return factionTags.Contains(t);
    }
    public bool HasFactionTag(string tagname)
    {
        return factionTags.Exists(t => t.Name.Equals(tagname, System.StringComparison.InvariantCultureIgnoreCase));
    }
    public bool ContainsAllFactionTags(List<FactionTag> checkingTags)
    {
        return new HashSet<FactionTag>(factionTags).IsSupersetOf(new HashSet<FactionTag>(checkingTags));
    }
}
