using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to create an asset menu for making consumable items
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]


public class ConsumableItem : Item
{
    public int healthModifier;

    public override void Use()
    {
        base.Use();
        HealthManager.instance.Eat(this);
        RemoveFromInventory();
        
    }
}

