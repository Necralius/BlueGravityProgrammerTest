using System.Collections.Generic;
using UnityEngine;
using NekraByte;

public class PaperdollController : MonoBehaviour
{
    #region - Singleton Pattern -
    public static PaperdollController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<BodyPart> Parts = new List<BodyPart>();

    public void EquipCloth(EquipableItem itemToEquip)
    {
        foreach(var part in Parts)
            if (part.Type == itemToEquip.itemBodyType) 
                part.ChangeBodyPart(itemToEquip.bodyPartPrefab);
    }

    public void DequipCloth(EquipableItem itemToDequip)
    {

    }
}