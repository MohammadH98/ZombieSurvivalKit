﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {

    /* Used to references the parent of all of the inventory slots
     */
    public Transform itemsParent;
    /* Used to reference the Canvas of the inventoryUI
     */
    public Canvas inventoryUI;

    /* Used to reference the the inventory instance
     */
    Inventory inventory;

    /* Used to references all of the inventory slots
     */
    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        // onItemChangedCcallBack triggers UpdateUI
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.enabled)
            {
                inventoryUI.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                inventoryUI.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }
	}

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].addItem(inventory.items[i]);
            } else
            {
                slots[i].clearSlot();
            }
        }
    }
}
