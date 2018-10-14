using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    /* Keeps track on when an item is added or removed in the inventory.
     * If a change has happened, trigger an event.
     * Will be used for updating the inventory UI later.
     */
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    /* The amount of space the inventory can have
     */
    public int space;

    /* The inventory list
     */
    public List<Item> items = new List<Item>();

    /* Used to add items to the inventory
     */
    public bool Add (Item item)
    {
        // Checks to see if the item being picked up is a default item
        if (!item.isDefaultItem)
        {
            // Checks if the inventory does not have enough space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in the inventory");
                return false;
            }
            // Add the item to the inventory
            items.Add(item);

            // Invoke a change to the inventory UI 
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            
            
        }

        return true;
    }

    /* Used to remove items to the inventory
     */
    public void Remove (Item item)
    {
        items.Remove(item);

        // Invoke a change to the inventory UI 
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
