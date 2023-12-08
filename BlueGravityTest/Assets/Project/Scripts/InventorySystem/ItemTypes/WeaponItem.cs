using UnityEngine;

/// <summary>
/// Represents a weapon item, extending the <see cref="EquipableItem"/> class.
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "NekraByte/Item/Equipable/Weapon/New Weapon Item")]
public class WeaponItem : EquipableItem
{
    [Header("Weapon Settings")]
    [Range(10f, 200f)] public int Damage = 10; // The damage value of the weapon.

    //The both hands weapon references.
    [Header("Weapon Parts Prefabs")]
    public GameObject weaponL_wrist = null;
    public GameObject weaponR_wrist = null;
}