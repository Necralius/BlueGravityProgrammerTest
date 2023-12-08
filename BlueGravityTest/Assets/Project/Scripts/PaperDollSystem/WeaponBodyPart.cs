using UnityEngine;

/// <summary>
/// Represents a body part for equipping weapons.
/// </summary>
public class WeaponBodyPart : BodyPart
{
    [Header("Weapon Positions")]
    [SerializeField] private Transform L_handPos = null;
    [SerializeField] private Transform R_handPos = null;

    [Header("Weapon Parts")]
    [SerializeField] private GameObject L_Part = null;
    [SerializeField] private GameObject R_Part = null;

    /// <summary>
    /// Initializes the weapon body part.
    /// </summary>
    protected override void InitializeBodyPart()
    {
        if (!Initialized)
        {
            // Check if any necessary components are assigned before initializing.
            if (L_handPos != null || R_handPos != null || L_Part != null || R_Part != null)
            {
                _aspectsManager.EquipOrUse(ItemEquiped);
                Initialized = true;
            }
        }
    }

    /// <summary>
    /// Changes the weapon body part to the specified new weapon.
    /// </summary>
    /// <param name="newBodyPart">The new weapon to equip.</param>
    public override void ChangeBodyPart(Item newBodyPart)
    {
        if (newBodyPart == null)
        {
            Debug.LogWarning("Invalid body part!");
            return;
        }

        // Check if the new body part is a valid weapon item.
        if (newBodyPart is WeaponItem)
        {
            WeaponItem weapon = (WeaponItem)newBodyPart;
            _aspectsManager.DequipItem(ItemEquiped);

            _aspectsManager.EquipOrUse(newBodyPart);

            // Destroy existing weapon parts.
            Destroy(L_Part);
            Destroy(R_Part);

            ItemEquiped = weapon;

            // Instantiate and attach new weapon parts to the specified positions.
            L_Part = Instantiate(weapon.weaponL_wrist, L_handPos);
            R_Part = Instantiate(weapon.weaponR_wrist, R_handPos);
        }
        else
        {
            // Log a warning if the new body part is not a valid weapon item.
            Debug.LogWarning("Invalid body part! Expected WeaponItem.");
        }
    }
}