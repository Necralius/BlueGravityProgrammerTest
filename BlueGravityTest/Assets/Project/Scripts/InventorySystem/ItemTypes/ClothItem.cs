using UnityEngine;

[CreateAssetMenu(fileName = "New Cloth Item", menuName = "NekraByte/Item/Cloth/New Cloth Item")]
public class ClothItem : Item
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //Model_ClothItem - (0.1)
    //State: Functional
    //This code represents an simple cloth item, that carries an unique information from the cloth item type.

    [Header("Paper Doll Data")]
    public int paperDollLayerID = 0; //-> This integer tells what papedoll layer the item is related to.
}