using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable: Is a Class that will be used to distinguish gameObjects that can be
/// interacted by the player.
/// Interactable inherits the MonoBehaviour Class
/// </summary>
public class Interactable : MonoBehaviour {
    

    public float radius;
    public bool isFocus = false;
    public Transform player;
    public bool hasInteracted = false;

    /// <summary>
    /// Interact(): Is a virtual void method that is re-programmable in all other
    /// classes that inherit the Interactable class.
    /// </summary>
    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }

    }

    public void onFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
