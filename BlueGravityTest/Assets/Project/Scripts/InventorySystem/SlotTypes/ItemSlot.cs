using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_Slot - (0.4)
    //State: Functional
    //This code represents an item slot model that holds all item data and basic behavior, also controlling the item UI.

    #region - Item Data and UI-
    [Header("Item Data")]
    public Image itemImage;
    public bool hasItem = false;
    public Item item;
    #endregion

    //============================Methods============================//

    #region - BuiltIn Methods -
    private void Start() => UpdateUI(); //-> Called in the game start
    #endregion

    #region - UI Update -
    public void UpdateUI() //This method updates all the slot UI, is called mainly on every item modification.
    {
        hasItem = item;
        itemImage.color = hasItem ? Color.white : new Color(255, 255, 255, 0);
        if (item == null) return;

        itemImage.sprite = item.Icon;
    }
    #endregion

    #region - Item Main Behaviors -
    public void AddItem(Item item) //This method add an item to the current slot and update the slot.
    {
        this.item = item;
        UpdateUI();
        ItemWasModified();
    }
    public Item GetAnRemoveItem() //This method remove an item to the current slot and update the slot.
    {
        Item thisItem   = item;
        item            = null;
        hasItem         = false;
        UpdateUI();
        ItemWasModified();
        return thisItem;
    }
    public virtual void ItemWasModified() { } //This method represent an callback, an kind of warning that tells every time that an item is modified, also this method is virtual, what means that this method can be overrided in other classes.
    public Item GetItem() => item; //This method represent a simple get that returns the current item on the slot.
    #endregion
}