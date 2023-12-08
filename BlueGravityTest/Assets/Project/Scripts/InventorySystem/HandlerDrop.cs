using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NekraliusDevelopmentStudio
{
    public class HandlerDrop : MonoBehaviour, IDropHandler
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //ItemDropHandler - (0.2)
        //State: Functional
        //This code represents an item drop action handler.

        public void OnDrop(PointerEventData eventData)//This method detects when the user get the mouse button up in the object area.
        {
            if (!GetComponentInParent<ItemSlot>()) return;

            if (!GetComponentInParent<ItemSlot>().hasItem) //First the system verifies if the slot is empty.
            {
                /*If it is empty, the system will get the correct item type to the correct slot type, Example Head_Items only can be attatched to HeadSlots and common
                 * slots.
                 * NOTE:Also the items that are HeadItems and SuitItems only be equiped in the body in his equivalent slots.
                 */

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
                    //If the slot is of an commum type (Model_Slot) the item is normaly added to the slot.
                    GetComponentInParent<ItemSlot>().AddItem(eventData.pointerDrag.GetComponent<HandlerItemDrag>()._currentSlot.GetAnRemoveItem());
                   
                }
                else return;
            }
        }
    }
}