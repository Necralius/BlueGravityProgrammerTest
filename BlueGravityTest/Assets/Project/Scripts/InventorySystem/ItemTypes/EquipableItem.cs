using UnityEngine;
using NekraByte;

/// <summary>
/// Represents an equipable item, such as clothing.
/// Extends the <see cref="Item"/> class.
/// </summary>
public class EquipableItem : Item
{
    // The body part type associated with this cloth item.
    [Header("Body Part Setting")]
    public EquipableType itemBodyType = EquipableType.Head;
}