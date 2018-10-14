using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    #region Singleton
    public static HealthManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    int maxHealth = 20;
    public int health;

    void Start()
    {
        health = 10;
    }

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
