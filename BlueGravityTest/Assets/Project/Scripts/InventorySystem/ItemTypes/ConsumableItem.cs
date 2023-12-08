using UnityEngine;

/// <summary>
/// Represents a consumable item, such as food.
/// Extends the <see cref="Item"/> class.
/// </summary>
[CreateAssetMenu(fileName = "New Food Item", menuName = "NekraByte/Item/Consumable/New Food Item")]
public class ConsumableItem : Item
{
    public int cureValue; //-> This information tells the nutritional value of the current food.
}