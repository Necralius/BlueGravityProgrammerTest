using UnityEngine.EventSystems;

/// <summary>
/// Represents a slot in the shop's inventory and handles click events. (MCV - Model)
/// </summary>
public class ShopSlot : ItemSlot, IPointerClickHandler
{
    /// <summary>
    /// Called when a pointer click (e.g., mouse click) is detected on the slot.
    /// </summary>
    /// <param name="eventData">Event data associated with the pointer click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the slot is part of a ShopView
        if (GetComponentInParent<ShopView>()) 
            GetComponentInParent<ShopView>().InspectItem(item); // Call the InspectItem method in the ShopView, passing the associated item
        else GetComponentInParent<ShopView>().RemoveInspectionItem();  // If there is no item to inspect, call RemoveInspectionItem in the parent ShopView
    }
}