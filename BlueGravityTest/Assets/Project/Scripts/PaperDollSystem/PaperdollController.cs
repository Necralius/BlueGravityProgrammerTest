using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the paperdoll system, responsible for equipping and dequipping cloth items on a character.
/// </summary>
public class PaperdollController : MonoBehaviour
{
    public static PaperdollController Instance;
    private void Awake() => Instance = this;

    public List<BodyPart> Parts = new List<BodyPart>(); // List of body parts in the paperdoll.

    /// <summary>
    /// Equips a cloth item on the corresponding body part.
    /// </summary>
    /// <param name="itemToEquip">The cloth item to equip.</param>
    public void EquipItem(EquipableItem itemToEquip)
    {
        foreach (var part in Parts)
        {
            if (part.Type == itemToEquip.itemBodyType)
            {
                part.ChangeBodyPart(itemToEquip);
                // Assuming that only one body part of the specified type can be equipped at a time,
                // so we break the loop after finding and equipping the correct part.
                break;
            }
        }
    }

    /// <summary>
    /// Dequips a equipable item from the corresponding body part.
    /// </summary>
    /// <param name="itemToDequip">The cloth item to dequip.</param>
    public void DequipItem(EquipableItem itemToDequip)
    {
        foreach (var part in Parts)
        {
            if (part.Type == itemToDequip.itemBodyType)
            {
                part.RemoveBodyPart();
                // Assuming that only one body part of the specified type can be equipped at a time,
                // so we break the loop after finding and dequipping the correct part.
                break;
            }
        }
    }
}