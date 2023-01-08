using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public _EquipmentType EquipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }
}
