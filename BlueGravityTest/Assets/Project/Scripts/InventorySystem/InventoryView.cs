using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IPointerClickHandler
{
    #region - Singleton Pattern -
    public static InventoryView Instance;
    private void Awake() => Instance = this;
    #endregion

    [Header("Item Inspection")]
    [SerializeField] private Image              itemImage       = null;
    [SerializeField] private TextMeshProUGUI    itemName        = null;
    [SerializeField] private TextMeshProUGUI    itemDescription = null;
    [SerializeField] private TextMeshProUGUI    itemPrice       = null;
    [SerializeField] private Item               selectedItem    = null;

    public void InspectItem(Item itemToInspect)
    {
        selectedItem            = itemToInspect;

        itemImage.sprite        = selectedItem.Icon;
        itemName.text           = selectedItem.Name;
        itemDescription.text    = selectedItem.Description;
        itemPrice.text          = $"Price: {selectedItem.SellCost}";
    }

    public void RemoveInspectionItem()
    {
        itemImage.sprite    = null;
        itemImage.color     = new Color(255, 255, 255, 0);

        itemName.text           = "Item Name";
        itemDescription.text    = "Item Description";
        itemPrice.text          = "Price";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RemoveInspectionItem();
        if (eventData.selectedObject != null && eventData.selectedObject.CompareTag("ItemSlot"))
        {
            if (eventData.selectedObject.GetComponent<HandlerItemDrag>()._currentSlot.hasItem)
                InspectItem(eventData.selectedObject.GetComponent<HandlerItemDrag>()._currentSlot.item);
        }
    }
}