using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSlot : ClothSlot
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_HeadSlot - (0.3)
    //State: Functional
    //This code represents an cloth slot item add behavior.
    //NOTE: This slot is limited to head items, like an hat, or an hair piece;

    public override void AddClothItem(ClothItem item)
    {
        //This method rerepesents an item add kind of override, ot only adds the item to the slot, but also equip the paper doll layer, thus equiping the selected cloths.
        AddItem(item);
        //PaperDollSystem.Instance.SetUpLayer(item.paperDollLayerID);
    }
}