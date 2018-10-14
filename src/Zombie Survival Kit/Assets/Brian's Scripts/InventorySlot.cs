using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    // A reference to the icon of the item being stored
    public Image icon;

    // A reference to the remove button on the inventory slot
    public Button removeButton;

    // A reference to the item being stored in the inventory UI and list
    Item item;

    /* Adds an item to the inventory UI
     */
    public void addItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    /* Clears the images on the inventory slot in the inventory UI
     */
    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    /* Removes the item in the inventory from the inventory list when
     * remove button is pressed
     */
    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    /* Uses the item in the inventory from the inventory list when
     * inventory slot button is pressed
     */
    public void useItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
