using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropItems : MonoBehaviour
{

    /* Creating the length of the Raycast
     */
    public float distanceToSee;

    /* Creating a public interactible variable that stores whatever the player had just focused on
     */
    public Interactable focus;

    /* Creating a public interactible variable
     */
    RaycastHit hit;

    /* Keeps a reference of the player
     */
    public GameObject player;

    /* Checks to see if the player is carrying anything
     */
    public bool carrying;

    /* The object the player is carrying
     */
    public GameObject carriedObject;

    /* Determines how far out from the player the object is being carried
     */
    public float carryingDistance;

    /* Determines the smoothness in which the object is carried and moved around
     */
    public float smooth;

    /* Use this for initialization
     */
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        /* Draws to screen the length of the raycast
         */
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        /* Checks the player is carrying something. If it is, then move carriedObject. Else, check for pickup instruction.
         */
        if (carrying)
        {
            carry(carriedObject);
            dropCheck();
        }
        else
        {
            pickup();
        }

        /* Checks if the player is focused on an interactable and checks the distance from the interactable to the player
         * If the player is outside the radius of the interactable, RemoveFocus()
         */
        if (focus != null && 
            Vector3.Distance(player.transform.position, focus.transform.position) > focus.radius)
        {
            RemoveFocus();
        }
    }

    void carry(GameObject o)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            o.GetComponent<Rigidbody>().isKinematic = false;
            o.transform.position = o.transform.position;

        }
        o.GetComponent<Rigidbody>().isKinematic = true;
        o.transform.position = Vector3.Lerp(o.transform.position,
            player.transform.position + player.transform.forward * carryingDistance,
            Time.deltaTime * smooth);
    }

    void dropCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
        }
    }

    void dropObject()
    {
        RemoveFocus();
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        // Brian:   Displays message in Log
        Debug.Log("I dropped a " + carriedObject.gameObject.name);
        carriedObject = null;
        
    }

    void pickup()
    {
        /* Creating an interaction with interactiable objects in game when user 
         * presses down the "E" key on their keyboard.
         */
        if (Input.GetKeyDown(KeyCode.E))
        {
            /* If the ray hits, do something
             */
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceToSee))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                /* Checks of the game object is interactable
                 */
                if (interactable != null)
                {
                    SetFocus(interactable);

                    ///* Checks the tag of the game object selected before doing something
                    // */
                    //if (hit.collider.tag == "Consumable")
                    //{
                    //    player.GetComponent<Inventory>().hasConsumable = true;
                    //    player.GetComponent<Inventory>().consumable = hit.transform.gameObject.GetComponent<ConsumableItem>().consumable;
                    //}
                    
                    Pickupable p = hit.collider.GetComponent<Pickupable>();
                    /* If the Object is interactable and pickupable, then the player 
                     * will carry the object
                     */
                    if (p != null)
                    {
                        carrying = true;
                        carriedObject = p.gameObject;
                        // Displays message in Log
                        Debug.Log("I picked up a " + hit.collider.gameObject.name);
                    }

                }

            }

        }
    }

    
    // Creates a focus on the object that the player interactes with
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.onDefocused();
            focus = newFocus;
        }
        
        newFocus.onFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.onDefocused();
        focus = null;
    } 
    
}

