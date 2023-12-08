using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [RequireComponent(typeof(InventoryView))]
    public class InventoryController : MonoBehaviour
    {
        //Direct Dependencies
        [HideInInspector] public InventoryView      invetoryView    = null;
        [HideInInspector] public ControllerTopDown  playerInstance  = null;

        [Header("Slots Data")]
        [SerializeField] private List<ItemSlot> _slots = new List<ItemSlot>();

        //============================Methods============================//

        private void Start()
        {
            invetoryView    = GetComponent<InventoryView>();
            playerInstance  = GetComponentInParent<ControllerTopDown>();
        }

        #region - Item Management -
        public bool AddItem<T>(T item) where T : Item
        {
            //This methods receive an generic type that inherit from Model_Item, that represent the item base class model, after the receive the method verifies if exists empty slots, and if exists, the item is added to the inventory.
            //NOTE: I am using generics for make an open model that can receive any type of item to be added, needing only that this item inherit from Model_Item.

            foreach (var slot in _slots)
            {
                if (slot.hasItem) continue;
                else
                {
                    slot.AddItem(item);
                    UpdateInventoryUI();
                    return true;
                }
            }
            Debug.Log("Invetory is full!");
            return false;
        }
        public void RemoveItem(ItemSlot slot) //This method remove the item from an especific slot passed as argument.
        {
            slot.GetAnRemoveItem();
            UpdateInventoryUI();
        }

        #endregion

        #region - UI Update -
        public void UpdateInventoryUI()
        {
            _slots.ForEach(slot => slot.UpdateUI());
            playerInstance.aspects.UpdateUI(false);
        }
        //This method run by all slots, and update his UIs one by one, is used basically in every item change ou inventory change.
        #endregion
    }
}