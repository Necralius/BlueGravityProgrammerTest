/// <summary>
/// Represents a cloth slot model that carries specific add behavior.
/// </summary>
public class ClothSlot : ItemSlot
{
    /// <summary>
    /// Adds a cloth item to the cloth slot, it is an virtual class, so it can be implemented in the future.
    /// </summary>
    /// <param name="item">The cloth item to be added.</param>
    public virtual void AddClothItem(EquipableItem item) { }
}