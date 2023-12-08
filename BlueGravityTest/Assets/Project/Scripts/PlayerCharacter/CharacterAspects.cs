using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAspects : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI _invHealth    = null;
    [SerializeField] private TextMeshProUGUI _invArmor     = null;
    [SerializeField] private TextMeshProUGUI _invDamage    = null;
    [SerializeField] private TextMeshProUGUI _invMoney     = null;

    [Header("Persistent UI")]
    [SerializeField] private TextMeshProUGUI    _persHealthText = null;
    [SerializeField] private TextMeshProUGUI    _persMoneyText  = null;

    [Header("Player Settings")]
    [SerializeField, Range(10, 300)]  private int healthPoints   = 150;
    [SerializeField, Range(10, 300)]  private int maxHealth      = 150;
    [SerializeField, Range(10, 200)]  private int armorPoints    = 20;

    [SerializeField, Range(10, 100)]  private int damage         = 0;
    [SerializeField, Range(0, 10000)] private int money          = 0;

    public int Health
    {
        get => healthPoints;
        set
        {
            if (value > MaxHealth) healthPoints = MaxHealth;
            else healthPoints = value;
        }
    }
    public int MaxHealth { get => maxHealth + armorPoints; }

    private void Start()
    {
        Health = maxHealth;
        UpdateUI(false);
    }

    public void UpdateUI(bool generalUpdate)
    {
        if (generalUpdate)
        {
            _invHealth.text         = $"Health: {Health}/{MaxHealth}";
            _invArmor.text          = $"Armor: {armorPoints}";
            _invDamage.text         = $"Damage: {damage}";
            _invMoney.text          = $"Money: {money}";

            _persHealthText.text    = $"Health: {Health}/{MaxHealth}";
            _persMoneyText.text     = $"Money: {money}";
        }
        else
        {
            _persHealthText.text = $"Health: {Health}/{MaxHealth}";
            _persMoneyText.text = $"Money: {money}";
        }
    }


    public void ConsumeItem(ConsumableItem itemToConsume)
    {
        Health += itemToConsume.cureValue;
        UpdateUI(false);
    }

    public void EquipArmor(EquipableItem equipableItem)
    {
        armorPoints += equipableItem.armorPoints;
        maxHealth += equipableItem.armorPoints;
        UpdateUI(true);
    }

    public void DequipArmor(EquipableItem equipableItem)
    {
        armorPoints -= equipableItem.armorPoints;
        maxHealth -= equipableItem.armorPoints;
        UpdateUI(true);
    }

    public bool MoneyTransaction(int value, bool add)
    {
        if (!add)
        {
            if ((money - value) > 0)
            {
                money -= value;
                UpdateUI(false);
                return true;
            }
            else return false;
        }
        else money += add ? value : -value;
        return true;
    }
}