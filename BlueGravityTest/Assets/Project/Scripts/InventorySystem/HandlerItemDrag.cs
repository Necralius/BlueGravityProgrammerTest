using NekraliusDevelopmentStudio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(HandlerDrop))]
public class HandlerItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //ItemDragHandler - (0.3)
    //State: Functional
    //This code represents an item drag handler that receive some BuiltIn unity mouse events interfaces that enables the use of the mouse as an very active input, this class handle the drag start, persistent drag and the drag end.

    private Image    _image => GetComponent<Image>();
    private Transform parentSave = null;

    [HideInInspector] public ItemSlot _currentSlot = null;

    private void OnEnable() //-> Method called when the item is seted to enable
    {
        _currentSlot = GetComponentInParent<ItemSlot>();
    }

    // ----------------------------------------------------------------------
    // Name: OnPointerClick (Method)
    // Desc: 
    // ----------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        /*Method called when the mouse pointer starts an drag on this object, also as all mouse event interfaces, the method receive an argument that has a lot of 
         * information about the mouse pointer, what makes this very usefull.
         * NOTE: Note that the image raycast target is disable when the drag action starts, the system is maded this way because when an item is beteween the target 
         * slot to drop and the mouse pointer, the slot cannot be detected, thus making the drag and drop action between slots impossible.
         */

        _image.raycastTarget = false;
        parentSave = transform.parent;
        transform.SetParent(transform.root);

        if (_currentSlot.item is ClothItem)
        {
            //This statement verifies if the item dragged is and cloth item, and if it is, the cloth item is unequiped from the player.
            ClothItem clothItem = (ClothItem)_currentSlot.item;
            //PaperDollSystem.Instance.DequipLayer(clothItem.paperDollLayerID);
        }
    }

    // ----------------------------------------------------------------------
    // Name: OnPointerClick (Method)
    // Desc: 
    // ----------------------------------------------------------------------
    public void OnDrag(PointerEventData eventData) //Method called every frame that the pointer is beeing dragged in the object.
    {
        //This method sets the gameobject position to the mouse position, making and great drag visual feeling.
        transform.position = InputManager.Instance.PointerPos;
    }

    // ----------------------------------------------------------------------
    // Name: OnPointerClick (Method)
    // Desc: 
    // ----------------------------------------------------------------------
    public void OnEndDrag(PointerEventData eventData) //Method called every time that the mouse pointer ends and drag action in the object.
    {
        //In hte method the item will return to his original position and the raycastTarget property is turned again to true.
        _image.raycastTarget        = true;
        transform.localPosition     = Vector2.zero;
        transform.SetParent(parentSave);
    }

    // ----------------------------------------------------------------------
    // Name: OnPointerClick (Method)
    // Desc: 
    // ----------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData) //This method detects an mouse click on the object
    {
        //TODO -> Item inspection 

        if (_currentSlot.hasItem)
            GameController.Instance._inventoryController._invetoryView.InspectItem(_currentSlot.item);
    }
}