using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the view for the shop, displaying and interacting with items. (MVC - View)
/// </summary>
public class ShopView : MonoBehaviour
{
    private ShopController _shopController = null;

    [Header("Item Inspection")]
    [SerializeField] private Image              itemImage       = null;
    [SerializeField] private TextMeshProUGUI    itemName        = null;
    [SerializeField] private TextMeshProUGUI    itemDescription = null;
    [SerializeField] private TextMeshProUGUI    itemPrice       = null;
    [SerializeField] private Sprite             nullItemSprt    = null;
    [SerializeField] private Item               selectedItem    = null;

    private void Start() => _shopController = GetComponent<ShopController>();

    /// <summary>
    /// Inspects the details of the selected item.
    /// </summary>
    /// <param name="itemToInspect">The item to inspect.</param>
    public void InspectItem(Item itemToInspect)
    {
        selectedItem = itemToInspect;

        itemImage.sprite = selectedItem.Icon;
        itemName.text = selectedItem.Name;
        itemDescription.text = selectedItem.Description;
        itemPrice.text = $"Price: {selectedItem.BuyCost}";
    }

    /// <summary>
    /// Removes the inspection details for the item.
    /// </summary>
    public void RemoveInspectionItem()
    {
        itemImage.sprite = nullItemSprt;

        itemName.text           = "Inspected Item Name";
        itemDescription.text    = "Item Description";
        itemPrice.text          = "Item Price";
    }

    /// <summary>
    /// Buys the currently inspected item.
    /// </summary>
    public void Buy()
    {
        if (selectedItem == null) return;
        _shopController.ItemBuyed(selectedItem);
        RemoveInspectionItem();
    }

    /// <summary>
    /// Sells all items in the shop inventory.
    /// </summary>
    public void Sell()
    {
        _shopController.SellAllItens();
        RemoveInspectionItem();
    }
}