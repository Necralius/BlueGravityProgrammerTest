using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour
{
    private ControllerTopDown   _playerInstance = null;
    private ShopView            _shopView       = null;

    [SerializeField] private List<ItemSlot> shopInventory = new List<ItemSlot>();

    private void Start()
    {
        _playerInstance = GetComponentInParent<ControllerTopDown>();
        _shopView       = GetComponent<ShopView>();
    }

    public void SellAllItens()
    {
        foreach(var slot in shopInventory) 
            if (slot.hasItem && slot.item != null) 
                _playerInstance.aspects.MoneyTransaction(slot.GetAnRemoveItem().SellCost, true);
    }

    public void ItemBuyed(Item itemBuyed)
    {
        bool finshedTransaction = false;
        foreach(var slot in shopInventory)
        {
            if (!slot.hasItem)
            {
                if (_playerInstance.aspects.MoneyTransaction(itemBuyed.BuyCost, false))
                {
                    slot.AddItem(itemBuyed);
                    finshedTransaction = true;
                }
                else break;
                break;
            }
        }

        if (!finshedTransaction) 
            Debug.Log("Shop inventory is full, or the player is withou enouth money!");
    }
}