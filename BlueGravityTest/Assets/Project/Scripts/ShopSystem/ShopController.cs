using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the shop functionality, including buying and selling items. (MVC - Controller)
/// </summary>
public class ShopController : MonoBehaviour
{
    private ControllerTopDown _playerInstance = null;
    private ShopView _shopView = null;

    [SerializeField] private List<ItemSlot> shopInventory = new List<ItemSlot>();

    private void Start()
    {
        _playerInstance = GetComponentInParent<ControllerTopDown>();
        _shopView = GetComponent<ShopView>();
    }

    /// <summary>
    /// Sells all items in the shop inventory, adding money to the player's aspects.
    /// </summary>
    public void SellAllItens()
    {
        foreach (var slot in shopInventory)
        {
            if (slot.hasItem && slot.item != null)
            {
                // Sell the item and add money to the player's aspects.
                _playerInstance.aspects.MoneyTransaction(slot.GetAnRemoveItem().SellCost, true);
            }
        }
    }

    /// <summary>
    /// Handles the transaction when a player buys an item.
    /// </summary>
    /// <param name="itemBuyed">The item the player bought.</param>
    public void ItemBuyed(Item itemBuyed)
    {
        bool finshedTransaction = false;

        foreach (var slot in shopInventory)
        {
            if (!slot.hasItem)
            {
                // Try to buy the item and add it to the shop inventory.
                if (_playerInstance.aspects.MoneyTransaction(itemBuyed.BuyCost, false))
                {
                    slot.AddItem(itemBuyed);
                    finshedTransaction = true;
                }
                else
                {
                    // Break the loop if the player doesn't have enough money.
                    break;
                }

                // Break the loop after successfully adding the item.
                break;
            }
        }

        if (!finshedTransaction)
        {
            Debug.Log("Shop inventory is full, or the player is without enough money!");
        }
    }
}