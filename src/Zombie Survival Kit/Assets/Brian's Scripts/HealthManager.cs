﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HealthManager: Is a class used to keep track of the player's health, and 
/// consists of methods that can affect the player's health.
/// </summary>
public class HealthManager : MonoBehaviour {

    /// <summary>
    /// Singleton: Is a region used to create an instance of the HealthManager
    /// class
    /// </summary>
    #region Singleton
    public static HealthManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    int maxHealth = 20;
    public int health;

    /// <summary>
    /// Start(): Is a void method used for initialization
    /// </summary>
    void Start()
    {
        health = 10;
    }

    /// <summary>
    /// Eat(ConsumableItem consumable): Is a void method that, upon use,
    /// enables the player to use the ConsumableItem to restore the player's
    /// health
    /// </summary>
    /// <param name="consumable">The ConsumableItem being used</param>
    public void Eat(ConsumableItem consumable)
    {
        if (health < 20)
        {
            if (health + consumable.healthModifier <= maxHealth)
            {
                health += consumable.healthModifier;
            } else
            {
                health = maxHealth;
            }

        }
    }


}
