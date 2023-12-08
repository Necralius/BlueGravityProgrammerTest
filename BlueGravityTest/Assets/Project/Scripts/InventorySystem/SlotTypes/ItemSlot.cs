using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an item slot model that holds item data, basic behavior, and controls item UI (MVC - Subsystem - Controller and View).
/// </summary>
public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")]
    public Image itemImage;
    public bool hasItem = false;
    public Item item;

    /// <summary>
    /// Called in the game start to update the UI.
    /// </summary>
    private void Start() => UpdateUI();

    /// <summary>
    /// Updates all the slot UI, mainly called on every item modification.
    /// </summary>
    public void UpdateUI()
    {
        hasItem = item;
        itemImage.color = hasItem ? Color.white : new Color(255, 255, 255, 0);
        if (item == null) return;

        itemImage.sprite = item.Icon;
    }

    /// <summary>
    /// Adds an item to the current slot and updates the slot.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    public void AddItem(Item item)
    {
        this.item = item;
        UpdateUI();
        ItemWasModified();
    }

    /// <summary>
    /// Removes an item from the current slot and updates the slot.
    /// </summary>
    /// <returns>The removed item.</returns>
    public Item GetAnRemoveItem()
    {
        Item itemRef = item;

        item = null;
        hasItem = false;

        UpdateUI();
        ItemWasModified();
        return itemRef;
    }

    /// <summary>
    /// Represents a callback indicating that an item is modified.
    /// </summary>
    public virtual void ItemWasModified() { }

    /// <summary>
    /// Gets the current item on the slot.
    /// </summary>
    /// <returns>The current item on the slot.</returns>
    public Item GetItem() => item;
}