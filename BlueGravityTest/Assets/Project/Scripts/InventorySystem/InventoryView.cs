using NekraliusDevelopmentStudio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController _inventoryController = null;

    [Header("Item Inspection")]
    [SerializeField] private Image              itemImage       = null;
    [SerializeField] private TextMeshProUGUI    itemName        = null;
    [SerializeField] private TextMeshProUGUI    itemDescription = null;
    [SerializeField] private TextMeshProUGUI    itemPrice       = null;
    [SerializeField] private Item               selectedItem    = null;
    [SerializeField] private ItemSlot           selectedItemSlot = null;
    [SerializeField] private Sprite             nullItemSprt    = null;

    private void Start()
    {
        _inventoryController    = GetComponent<InventoryController>();
    }

    public void InspectItem(ItemSlot slotSave)
    {
        selectedItem            = slotSave.item;
        selectedItemSlot        = slotSave;

        itemImage.sprite        = selectedItem.Icon;
        itemName.text           = selectedItem.Name;
        itemDescription.text    = selectedItem.Description;
        itemPrice.text          = $"Price: {selectedItem.SellCost}";
    }

    public void RemoveInspectionItem()
    {
        itemImage.sprite        = nullItemSprt;

        itemName.text           = "Inspected Item Name";
        itemDescription.text    = "Item Description";
        itemPrice.text          = "Item Price";
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    RemoveInspectionItem();
    //    if (eventData.selectedObject != null && eventData.selectedObject.CompareTag("ItemSlot"))
    //    {
    //        if (eventData.selectedObject.GetComponent<HandlerItemDrag>()._currentSlot.hasItem)
    //            InspectItem(eventData.selectedObject.GetComponent<HandlerItemDrag>()._currentSlot);
    //    }
    //}

    public void RemoveItem()
    {
        if (selectedItem != null)
        {
            if (selectedItem is EquipableItem)
                _inventoryController.playerInstance.paperDoll.DequipCloth(selectedItem as EquipableItem);

            _inventoryController.RemoveItem(selectedItemSlot);
        }
    }

    public void EquipOrUseItem()
    {
        if (selectedItem != null)
        {
            if (selectedItem is EquipableItem) 
                _inventoryController.playerInstance.paperDoll.EquipCloth(selectedItem as EquipableItem);

            if (selectedItem is ConsumableItem)
                _inventoryController.playerInstance.aspects.ConsumeItem(selectedItem as ConsumableItem);
        }
    }
}