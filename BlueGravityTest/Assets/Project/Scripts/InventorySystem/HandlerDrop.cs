using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the drop action for draggable items.
/// </summary>
public class HandlerDrop : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// Detects when the user releases the mouse button in the object area.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnDrop(PointerEventData eventData)
    {
        // Ensure the object has a parent ItemSlot component.
        if (!GetComponentInParent<ItemSlot>()) return;

        // Check if the slot is empty.
        if (!GetComponentInParent<ItemSlot>().hasItem)
        {
            // Get the correct item type for the correct slot type.
            if (GetComponentInParent<HeadSlot>())
            {
                Item newItem = eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetItem();
                if (newItem is HeadItem)
                    GetComponentInParent<HeadSlot>().AddClothItem((HeadItem)eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetAnRemoveItem());
            }
            else if (GetComponentInParent<SuitSlot>())
            {
                Item newItem = eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetItem();
                if (newItem is SuitItem)
                    GetComponentInParent<SuitSlot>().AddClothItem((SuitItem)eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetAnRemoveItem());
            }
            else if (GetComponentInParent<ConsumableSlot>())
            {
                Item newItem = eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetItem();
                if (newItem is ConsumableItem)
                    GetComponentInParent<ConsumableSlot>().AddItem(eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetAnRemoveItem());
            }
            else if (GetComponentInParent<ItemSlot>())
            {
                // If the slot is of a common type (Model_Slot), the item is normally added to the slot.
                GetComponentInParent<ItemSlot>().AddItem(eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetAnRemoveItem());
            }
            else return;
        }
    }
}