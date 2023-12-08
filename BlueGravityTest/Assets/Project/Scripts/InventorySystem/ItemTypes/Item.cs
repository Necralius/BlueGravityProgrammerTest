using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "NekraByte/Item/New Item")]
public class Item : ScriptableObject
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_Item - (0.1)
    //State: Functional
    //This code represents an base item model that carrys all the item base informations.

    public string Name          = "ItemName";
    [TextArea] public string Description   = "ItemDescription"; 
    public Sprite Icon          = null;

    [Range(10, 1000)] public int SellCost   = 10;
    [Range(10, 1000)] public int BuyCost    = 10;

    //[HideInInspector] public ItemSlot slotSave = null;
}