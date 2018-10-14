using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    /* Will be used later to show that an equipment change has occured on the character.
     * Does nothing for now.
     */
    public delegate void OnEquipmentChanged(EquipmentItem newEquipment, EquipmentItem oldEquippment);
    public OnEquipmentChanged onEquipmentChanged;

    public EquipmentItem[] equippedItems;
    Inventory inventory;

	void Start () {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        equippedItems = new EquipmentItem[numSlots];

        inventory = Inventory.instance;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void Equip(EquipmentItem newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;

        EquipmentItem oldEquipment = null;

        if(equippedItems[slotIndex] != null)
        {
            oldEquipment = equippedItems[slotIndex];
            inventory.Add(oldEquipment);
        }

        equippedItems[slotIndex] = newEquipment;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }
    }

    public void Unequip (int SlotIndex)
    {
        if(equippedItems[SlotIndex] != null)
        {
            EquipmentItem oldEquipment = equippedItems[SlotIndex];
            inventory.Add(oldEquipment);

            equippedItems[SlotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < equippedItems.Length; i++)
        {
            Unequip(i);
        }
    }

}
