using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the visual representation and interaction of the player's inventory. (MVC - View)
/// </summary>
public class InventoryView : MonoBehaviour
{
    private InventoryController _inventoryController = null;

    [Header("Item Inspection")]
    [SerializeField] private Image              itemImage           = null;
    [SerializeField] private TextMeshProUGUI    itemName            = null;
    [SerializeField] private TextMeshProUGUI    itemDescription     = null;
    [SerializeField] private TextMeshProUGUI    itemPrice           = null;
    [SerializeField] private Item               selectedItem        = null;
    [SerializeField] private ItemSlot           selectedItemSlot    = null;
    [SerializeField] private Sprite             nullItemSprt        = null;

    private void Start()
    {
        // Get a reference to the InventoryController component
        _inventoryController = GetComponent<InventoryController>();
    }

    /// <summary>
    /// Inspects the details of a selected item in the inventory.
    /// </summary>
    /// <param name="slotSave">The slot containing the item to be inspected.</param>
    public void InspectItem(ItemSlot slotSave)
    {
        selectedItem = slotSave.item;
        selectedItemSlot = slotSave;

        // Update UI elements with information about the selected item
        itemImage.sprite            = selectedItem.Icon;
        itemName.text               = selectedItem.Name;
        itemDescription.text        = selectedItem.Description;
        itemDescription.alignment   = TextAlignmentOptions.Left;
        itemPrice.text              = $"Price: {selectedItem.SellCost}";
    }

    /// <summary>
    /// Removes the inspection details of the currently selected item.
    /// </summary>
    public void RemoveInspectionItem()
    {
        // Reset UI elements to default values
        itemImage.sprite = nullItemSprt;

        itemName.text               = "Inspected Item Name";
        itemDescription.text        = "Item Description";
        itemDescription.alignment   = TextAlignmentOptions.Center;
        itemPrice.text              = "Item Price";
    }

    /// <summary>
    /// Removes the currently selected item from the inventory.
    /// </summary>
    public void RemoveItem()
    {
        if (selectedItem != null)
        {
            // Dequip cloth items before removing
            if (selectedItem is EquipableItem)
                _inventoryController.playerInstance.paperDoll.DequipItem(selectedItem as EquipableItem);

            // Remove the item from the inventory
            _inventoryController.RemoveItem(selectedItemSlot);
        }
    }

    /// <summary>
    /// Equips or uses the currently selected item.
    /// </summary>
    public void EquipOrUseItem()
    {
        if (selectedItem != null)
        {
            // Equip cloth items
            if (selectedItem is EquipableItem)
            {
                _inventoryController.playerInstance.paperDoll.EquipItem(selectedItem as EquipableItem);
                selectedItem        = null;
                selectedItemSlot    = null;
            }

            // Use consumable items
            if (selectedItem is ConsumableItem)
            {
                _inventoryController.playerInstance.aspects.EquipOrUse(selectedItem as ConsumableItem);
                _inventoryController.RemoveItem(selectedItemSlot);
                selectedItem        = null;
                selectedItemSlot    = null;
            }
        }
    }
}