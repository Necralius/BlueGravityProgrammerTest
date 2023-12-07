using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuitSlot : ClothSlot
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_SuitSlot - (0.1)
    //State: Functional
    //This code represents an cloth slot item add behavior.
    //This slot is limited to suit items, like complete costumes.

    public override void AddClothItem(EquipableItem item)
    {
        //This method rerepesents an item add kind of override, ot only adds the item to the slot, but also equip the paper doll layer, thus equiping the selected cloths.
        AddItem(item);
        //PaperDollSystem.Instance.SetUpLayer(item.paperDollLayerID);
    }
}