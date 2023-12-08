using UnityEngine;

/// <summary>
/// Represents a type of equipable item that can be worn as clothing.
/// Extends the <see cref="EquipableItem"/> class.
/// </summary>
public class ClothItem : EquipableItem
{
    // The prefab for the body part associated with this cloth item.
    [Header("Part Prefab")]
    public GameObject bodyPartPrefab = null;

    // The amount of armor points provided by this cloth item.
    public int armorPoints = 0;
}
