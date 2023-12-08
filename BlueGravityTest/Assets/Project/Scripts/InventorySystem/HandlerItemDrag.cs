using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles the dragging and clicking events for an item.
/// </summary>
[RequireComponent(typeof(Image), typeof(HandlerDrop))]
public class HandlerItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler
{
    // Image component of the actual GameObject.
    private Image _image => GetComponent<Image>();

    // Reference to the InventoryController.
    private InventoryController _invController = null;

    // Parent of the item before the drag action.
    private Transform _parentSave = null;

    // Reference to the current item slot.
    [HideInInspector] public ItemSlot _currentSlot = null;

    private void Start()
    {
        // Get references to the current item slot and the InventoryController.
        _currentSlot    = GetComponentInParent<ItemSlot>();
        _invController  = GetComponentInParent<ControllerTopDown>().invController;
    }

    /// <summary>
    /// Called when the mouse drag action begins on this object.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Disable raycast target to allow other UI elements to be detected during drag.
        _image.raycastTarget = false;
        _parentSave = GetComponentInParent<ItemSlot>().transform;
        transform.SetParent(GetComponentInParent<Canvas>().transform);

        // If the dragged item is a cloth item, unequip it from the player.
        if (_currentSlot.item is EquipableItem)
        {
            EquipableItem clothItem = (EquipableItem)_currentSlot.item;
            PaperdollController.Instance.DequipItem(clothItem);
        }

        // Inspect the item if the slot has an item.
        if (_currentSlot.hasItem)
            _invController.invetoryView.InspectItem(_currentSlot);
    }

    /// <summary>
    /// Called every frame that the pointer is being dragged over the object.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnDrag(PointerEventData eventData)
    {
        // Set the position of the game object to the mouse position for a smooth drag effect
        transform.position = InputManager.Instance.PointerPos;
    }

    /// <summary>
    /// Called when the mouse drag action ends over the object.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // Return the item to its original position and enable raycast target.
        transform.SetParent(_parentSave);
        _image.raycastTarget = true;
        transform.localPosition = Vector2.zero;
    }

    /// <summary>
    /// Called when a mouse click is detected on the object.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // TODO: Item inspection

        // Inspect the item if the slot has an item
        if (_currentSlot.hasItem)
            _invController.invetoryView.InspectItem(_currentSlot);
    }
}