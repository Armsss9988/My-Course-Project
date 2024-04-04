using System.Collections.Generic;
using UnityEngine;

public static class ObjectTagCheck
{
    public static bool HasTypeTag(this GameObject obj, TypeTag tag)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasTypeTag(tag);
    }
    public static bool HasTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasTypeTag(tagName);
    }
    public static bool HasTypeTags(this GameObject obj, List<TypeTag> tagscheck)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.ContainsAllTypeTags(tagscheck);
    }


    public static bool HasAttackTypeTag(this GameObject obj, AttackTypeTag tag)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasAttackTypeTag(tag);
    }
    public static bool HasAttackTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasAttackTypeTag(tagName);
    }
    public static bool HasAttackTypeTags(this GameObject obj, List<AttackTypeTag> tagscheck)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.ContainsAllAttackTypeTags(tagscheck);
    }


    public static bool HasDamageableTypeTag(this GameObject obj, AttackTypeTag tag)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasDamageableTypeTag(tag);
    }
    public static bool HasDamageableTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasDamageableTypeTag(tagName);
    }
    public static bool HasDamageableTypeTags(this GameObject obj, List<AttackTypeTag> tagscheck)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.ContainsAllDamageableTypeTags(tagscheck);
    }


    public static bool HasFactionTag(this GameObject obj, FactionTag tag)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasFactionTag(tag);
    }
    public static bool HasFactionTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.HasFactionTag(tagName);
    }
    public static bool HasFactionTags(this GameObject obj, List<FactionTag> tagscheck)
    {
        return obj.TryGetComponent<Tags>(out var tags) && tags.ContainsAllFactionTags(tagscheck);
    }

    public static bool HasTargetTypeTag(this GameObject obj, TypeTag tag)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasTypeTag(tag);
    }
    public static bool HasTargetTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasTypeTag(tagName);
    }
    public static bool HasTargetTypeTags(this GameObject obj, List<TypeTag> tagscheck)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.ContainsAllTypeTags(tagscheck);
    }


    public static bool HasTargetAttackTypeTag(this GameObject obj, AttackTypeTag tag)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasAttackTypeTag(tag);
    }
    public static bool HasTargetAttackTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasAttackTypeTag(tagName);
    }
    public static bool HasTargetAttackTypeTags(this GameObject obj, List<AttackTypeTag> tagscheck)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.ContainsAllAttackTypeTags(tagscheck);
    }


    public static bool HasTargetDamageableTypeTag(this GameObject obj, AttackTypeTag tag)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasDamageableTypeTag(tag);
    }
    public static bool HasTargetDamageableTypeTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasDamageableTypeTag(tagName);
    }
    public static bool HasTargetDamageableTypeTags(this GameObject obj, List<AttackTypeTag> tagscheck)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.ContainsAllDamageableTypeTags(tagscheck);
    }


    public static bool HasTargetFactionTag(this GameObject obj, FactionTag tag)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasFactionTag(tag);
    }
    public static bool HasTargetFactionTag(this GameObject obj, string tagName)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.HasFactionTag(tagName);
    }
    public static bool HasTargetFactionTags(this GameObject obj, List<FactionTag> tagscheck)
    {
        return obj.TryGetComponent<TargetTags>(out var tags) && tags.ContainsAllFactionTags(tagscheck);
    }


    public static bool IsTargetThisObject(this GameObject obj, GameObject target)
    {
        if (target == null) return false;
        if (target.TryGetComponent<Tags>(out var tag))
        {
            if (tag.typeTags == null || tag.factionTags == null) return false;
            if (!obj.HasTargetTypeTags(tag.typeTags))
            {
                return false;
            }
            if (!obj.HasTargetFactionTags(tag.factionTags))
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        if (obj.TryGetComponent<TargetTags>(out var targetTag))
        {
            if (targetTag.damageableTypeTags == null) return false;
            if (!target.HasDamageableTypeTags(targetTag.damageableTypeTags))
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

}
