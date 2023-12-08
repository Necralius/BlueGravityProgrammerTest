using UnityEngine;
using NekraByte;

public class EquipableItem : Item
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_ClothItem - (0.1)
    //State: Functional
    //This code represents an simple cloth item, that carries an unique information from the cloth item type.

    public GameObject   bodyPartPrefab  = null;
    public ClothType    itemBodyType    = ClothType.Head;
    public int          armorPoints     = 0;
}