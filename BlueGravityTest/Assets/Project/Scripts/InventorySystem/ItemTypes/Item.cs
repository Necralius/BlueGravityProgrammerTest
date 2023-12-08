using UnityEngine;

/// <summary>
/// Represents a base item model that carries information about an item.
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "NekraByte/Item/New Item")]
public class Item : ScriptableObject
{

    [Header("Item Settings")]
    public string            Name           = "ItemName";
    [TextArea] public string Description    = "ItemDescription";
    public Sprite            Icon           = null; // The icon representing the item.

    [Header("Item Pricing")]
    [Range(10, 1000)] public int SellCost   = 10; // The selling price of the item.
    [Range(10, 1000)] public int BuyCost    = 10; // The buying cost of the item.
}