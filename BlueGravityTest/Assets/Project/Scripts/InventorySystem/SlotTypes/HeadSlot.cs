/// <summary>
/// Represents a slot specifically for head cloth items.
/// </summary>
public class HeadSlot : ClothSlot
{
    /// <summary>
    /// Adds a head cloth item to the slot and equips it on the paper doll layer.
    /// </summary>
    /// <param name="item">The head cloth item to be added.</param>
    public override void AddClothItem(EquipableItem item)
    {
        // This method overrides the base method to add a head cloth item.
        AddItem(item);
    }
}