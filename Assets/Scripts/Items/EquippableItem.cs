using UnityEngine;
using PlagueTrain.CharacterStats;

public enum _EquipmentType
{
    Helmet, 
    Chest, 
    Pants, 
    Shoes, 
    Weapon, 
    Pickaxe,
    Axe,
    Accessory,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    [Space]
    public _EquipmentType EquipmentType;
    [Space]
    public int StrengthBonus;
    public int DefenseBonus;
    public int AgilityBonus;
    public int VitalityBonus;
    [Space]
    public float StrenghtPercentBonus;
    public float DefensePercentBonus;
    public float AgilityPercentBonus;
    public float VitalityPercentBonus;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }
    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(Character c)
    {
        //Flat Values
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModifierType.Flat, this));
        if (DefenseBonus != 0)
            c.Defense.AddModifier(new StatModifier(DefenseBonus, StatModifierType.Flat, this));
        if (AgilityBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModifierType.Flat, this));
        if (VitalityBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModifierType.Flat, this));

        //Percent Values
        if (StrenghtPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrenghtPercentBonus, StatModifierType.PercentMulti, this));
        if (DefensePercentBonus != 0)
            c.Defense.AddModifier(new StatModifier(DefensePercentBonus, StatModifierType.PercentMulti, this));
        if (AgilityPercentBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModifierType.PercentMulti, this));
        if (VitalityPercentBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModifierType.PercentMulti, this));
    }
    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Defense.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Vitality.RemoveAllModifiersFromSource(this);
    }
}
