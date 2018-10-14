using UnityEngine;

public class ItemStore : Interactable {

    public Item item;

	public override void Interact()
    {
        base.Interact();
        StoreItem();
    }

    void StoreItem()
    {
        /* Add item to inventory
         */
        bool wasPickedUp = Inventory.instance.Add(item);
        /* If item was picked up successfully, the gameobject of the item is destroyed from scene
         */
        if (wasPickedUp)
        {
            Debug.Log("Picked up " + item.name);
            Destroy(gameObject);
        }
            
    }
}
