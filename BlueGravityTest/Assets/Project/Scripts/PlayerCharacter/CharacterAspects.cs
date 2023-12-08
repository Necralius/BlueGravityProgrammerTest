using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents the aspects of a character, including health, armor, damage, and money.
/// </summary>
public class CharacterAspects : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI _invHealth = null;
    [SerializeField] private TextMeshProUGUI _invArmor = null;
    [SerializeField] private TextMeshProUGUI _invDamage = null;
    [SerializeField] private TextMeshProUGUI _invMoney = null;

    [Header("Persistent UI")]
    [SerializeField] private TextMeshProUGUI _persHealthText = null;
    [SerializeField] private TextMeshProUGUI _persMoneyText = null;

    [Header("Player Settings")]
    [SerializeField, Range(10, 300)] private int healthPoints = 150;
    [SerializeField, Range(10, 300)] private int maxHealth = 150;
    [SerializeField, Range(10, 200)] private int armorPoints = 20;

    [SerializeField, Range(10, 100)] private int damage = 0;
    [SerializeField, Range(0, 10000)] private int money = 0;

    public int Damage { get => damage; }

    public int Health
    {
        get => healthPoints;
        set
        {
            // Ensure health doesn't exceed the maximum and handle death if health reaches zero.
            if (value > MaxHealth) healthPoints = MaxHealth;
            else if (value <= 0)
            {
                healthPoints = 0;
                Death();
            }
            else healthPoints = value;
        }
    }

    public int MaxHealth
    {
        get => maxHealth + armorPoints;
        set
        {
            maxHealth = value;
            healthPoints = maxHealth;
        }
    }

    private void Start()
    {
        Health = maxHealth;
        UpdateUI(false);
    }

    /// <summary>
    /// Updates the UI based on the specified condition.
    /// </summary>
    /// <param name="generalUpdate">True for a general update, false for a partial update.</param>
    public void UpdateUI(bool generalUpdate)
    {
        if (generalUpdate)
        {
            // Update all UI elements.
            _invHealth.text     = $"Health: {Health}/{MaxHealth}";
            _invArmor.text      = $"Armor: {armorPoints}";
            _invDamage.text     = $"Damage: {damage}";
            _invMoney.text      = $"Money: {money}";

            _persHealthText.text    = $"Health: {Health}/{MaxHealth}";
            _persMoneyText.text     = $"Money: {money}";
        }
        else
        {
            // Update persistent UI elements.
            _persHealthText.text    = $"Health: {Health}/{MaxHealth}";
            _persMoneyText.text     = $"Money: {money}";
        }
    }

    /// <summary>
    /// Handles the equip or use action for the specified item.
    /// </summary>
    /// <param name="itemInteracted">The item to interact with.</param>
    public void EquipOrUse(Item itemInteracted)
    {
        if (itemInteracted is ClothItem)
        {
            ClothItem item = (ClothItem)itemInteracted;

            armorPoints += item.armorPoints;
            maxHealth   += item.armorPoints;
            UpdateUI(true);
        }
        else if (itemInteracted is ConsumableItem)
        {
            ConsumableItem item = (ConsumableItem)itemInteracted;

            Health += item.cureValue;
            UpdateUI(true);
        }
        else if (itemInteracted is WeaponItem)
        {
            WeaponItem item = (WeaponItem)itemInteracted;
            damage += item.Damage;

            // Update the weapon damage if a weapon is equipped.
            if (GetComponentInChildren<Weapon>())
                GetComponentInChildren<Weapon>().Damage = damage;

            UpdateUI(true);
        }
    }

    /// <summary>
    /// Handles the dequip action for the specified item.
    /// </summary>
    /// <param name="itemInteracted">The item to dequip.</param>
    public void DequipItem(Item itemInteracted)
    {
        if (itemInteracted is ClothItem)
        {
            ClothItem item = (ClothItem)itemInteracted;

            armorPoints -= item.armorPoints;
            maxHealth   += item.armorPoints;
            UpdateUI(true);
        }
        else if (itemInteracted is WeaponItem)
        {
            WeaponItem item  = (WeaponItem)itemInteracted;
            damage          -= item.Damage;
            UpdateUI(true);
        }
    }

    /// <summary>
    /// Performs a money transaction.
    /// </summary>
    /// <param name="value">The transaction value.</param>
    /// <param name="add">True to add money, false to deduct money.</param>
    /// <returns>True if the transaction is successful, false otherwise.</returns>
    public bool MoneyTransaction(int value, bool add)
    {
        if (!add)
        {
            // Deduct money if there is enough balance.
            if ((money - value) > 0)
            {
                money -= value;
                UpdateUI(false);
                return true;
            }
            else return false;
        }
        else
        {
            // Add or deduct money based on the specified condition.
            money += add ? value : -value;
            UpdateUI(false);
            return true;
        }
    }

    /// <summary>
    /// Handles the character's death.
    /// </summary>
    public void Death() => GetComponent<ControllerTopDown>().DeathBehavior();

    /// <summary>
    /// Inflicts damage to the character.
    /// </summary>
    /// <param name="damageValue">The amount of damage to inflict.</param>
    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        UpdateUI(false);
    }
}