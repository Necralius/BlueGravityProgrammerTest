using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the player's inventory. (MVC - Controller)
/// </summary>
[RequireComponent(typeof(InventoryView))]
public class InventoryController : MonoBehaviour
{
    // Direct Dependencies
    [HideInInspector] public InventoryView invetoryView         = null;
    [HideInInspector] public ControllerTopDown playerInstance   = null;

    [Header("Slots Data")]
    [SerializeField] private List<ItemSlot> _slots = new List<ItemSlot>();

    private void Start()
    {
        // Get references to the InventoryView and ControllerTopDown components
        invetoryView    = GetComponent<InventoryView>();
        playerInstance  = GetComponentInParent<ControllerTopDown>();
    }

    #region - Item Management -

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <typeparam name="T">Type of the item.</typeparam>
    /// <param name="item">Item to be added.</param>
    /// <returns>True if the item was successfully added, false if the inventory is full.</returns>
    public bool AddItem<T>(T item) where T : Item
    {
        // This method adds an item of the specified type to the inventory if there's an empty slot.
        // It uses generics to make the method open to any type that inherits from Item.

        foreach (var slot in _slots)
        {
            if (slot.hasItem) continue;
            else
            {
                slot.AddItem(item);
                UpdateInventoryUI();
                return true;
            }
        }
        Debug.Log("Inventory is full!");
        return false;
    }

    /// <summary>
    /// Removes an item from a specific slot.
    /// </summary>
    /// <param name="slot">The slot from which the item will be removed.</param>
    public void RemoveItem(ItemSlot slot)
    {
        // This method removes the item from a specific slot.
        slot.GetAnRemoveItem();
        UpdateInventoryUI();
    }

    #endregion

    #region - UI Update -

    /// <summary>
    /// Updates the UI of the entire inventory.
    /// </summary>
    public void UpdateInventoryUI()
    {
        // This method updates the UI of all slots and the player's aspects UI.
        _slots.ForEach(slot => slot.UpdateUI());
        playerInstance.aspects.UpdateUI(false);
    }

    #endregion
}