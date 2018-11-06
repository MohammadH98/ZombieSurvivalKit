using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    /// <summary>
    /// Singleton: Is a region used to create an instance of the HealthManager
    /// class
    /// </summary>
    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Use this for initialization
    void Start ()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        curHealth = maxHealth;
	}

    void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null)
        {
            armour.AddToStat(newItem.defenceModifier);
            dmg.AddToStat(newItem.attackModifier);
        }

        if (oldItem != null)
        {
            armour.RemoveFromStat(oldItem.defenceModifier);
            dmg.RemoveFromStat(oldItem.attackModifier);
        }

    }

    /// <summary>
    /// Eat(ConsumableItem consumable): Is a void method that, upon use,
    /// enables the player to use the ConsumableItem to restore the player's
    /// health
    /// </summary>
    /// <param name="consumable">The ConsumableItem being used</param>
    public void Eat(ConsumableItem consumable)
    {
        if (curHealth < maxHealth)
        {
            if (curHealth + consumable.healthModifier <= maxHealth)
            {
                curHealth += consumable.healthModifier;
            }
            else
            {
                curHealth = maxHealth;
            }

        }
    }

    public override void Die()
    {
        base.Die();
        //Kill the player, game over screen, reset
        HealthManager.instance.KillPlayer();
    }

}
