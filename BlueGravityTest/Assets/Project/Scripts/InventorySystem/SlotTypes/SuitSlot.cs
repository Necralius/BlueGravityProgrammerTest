/// <summary>
/// Represents a slot for suit items, limited to complete costumes.
/// </summary>
public class SuitSlot : ClothSlot
{
    /// <summary>
    /// Adds a suit item to the slot, equipping the paper doll layer.
    /// </summary>
    /// <param name="item">The suit item to be added.</param>
    public override void AddClothItem(EquipableItem item)
    {
        // This method represents an item add override. It not only adds the item to the slot
        AddItem(item);
    }
}