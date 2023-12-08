using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    private ShopController _shopController = null;

    [Header("Item Inspection")]
    [SerializeField] private Image              itemImage           = null;
    [SerializeField] private TextMeshProUGUI    itemName            = null;
    [SerializeField] private TextMeshProUGUI    itemDescription     = null;
    [SerializeField] private TextMeshProUGUI    itemPrice           = null;
    [SerializeField] private Sprite             nullItemSprt        = null;
    [SerializeField] private Item               selectedItem        = null;

    private void Start()
    {
        _shopController = GetComponent<ShopController>();
    }

    public void InspectItem(Item itemToInspect)
    {
        selectedItem = itemToInspect;

        itemImage.sprite        = selectedItem.Icon;
        itemName.text           = selectedItem.Name;
        itemDescription.text    = selectedItem.Description;
        itemPrice.text          = $"Price: {selectedItem.BuyCost}";
    }

    public void RemoveInspectionItem()
    {
        itemImage.sprite        = nullItemSprt;

        itemName.text           = "Inspected Item Name";
        itemDescription.text    = "Item Description";
        itemPrice.text          = "Item Price";
    }

    public void Buy()
    {
        if (selectedItem == null) return;
        _shopController.ItemBuyed(selectedItem);
        RemoveInspectionItem();
    }
    public void Sell()
    {
        _shopController.SellAllItens();
        RemoveInspectionItem();
    }


}