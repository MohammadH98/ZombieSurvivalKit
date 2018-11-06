using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    HealthManager playerManager;
    CharacterStats enemyStats;

    private void Start()
    {
        playerManager = HealthManager.instance;
        enemyStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.Attack(enemyStats);
        }
    }
}
