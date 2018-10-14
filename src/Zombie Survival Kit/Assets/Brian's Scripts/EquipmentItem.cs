using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class EquipmentItem : Item
{
    public EquipmentSlot equipSlot;

    public int attackModifier;
    public int defenceModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();

    }
   
}

public enum EquipmentSlot { Head, Chest, Legs, Primaryhand, Offhand, Feet }
