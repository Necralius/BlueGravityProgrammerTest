using UnityEngine;
using NekraByte;

/// <summary>
/// Represents an equipable body part that can be attached to a character.
/// </summary>
public class BodyPart : MonoBehaviour
{
    // Direct Dependencies
    protected CharacterAspects _aspectsManager;

    [Header("Equipable Body Part")]
    [SerializeField] protected GameObject Part = null;

    [Header("Equipable Settings and Data")]
    [SerializeField] public    EquipableType    Type            = EquipableType.Suit;
    [SerializeField] protected EquipableItem    ItemEquiped     = null;
    [SerializeField] protected EquipableItem    defaultBodyPart = null;

    // Private Data
    private Vector3 Position        = Vector3.zero;
    private Vector3 Scale           = Vector3.zero;
    protected bool  Initialized     = false;

    private void Start()
    {
        _aspectsManager = GetComponentInParent<CharacterAspects>();
        InitializeBodyPart();
    }

    /// <summary>
    /// Initializes the body part, equipping it if a valid part and item are provided.
    /// </summary>
    protected virtual void InitializeBodyPart()
    {
        if (!Initialized)
        {
            if (Part != null && ItemEquiped != null)
            {
                _aspectsManager.EquipOrUse(ItemEquiped);

                Position    = Part.transform.localPosition;
                Scale       = Part.transform.localScale;
                Initialized = true;
            }
        }
    }

    /// <summary>
    /// Changes the current body part to a new one.
    /// </summary>
    /// <param name="newBodyPart">The new body part item.</param>
    public virtual void ChangeBodyPart(Item newBodyPart)
    {
        if (newBodyPart == null || !(newBodyPart is ClothItem))
        {
            Debug.LogWarning("Invalid body part!");
            return;
        }

        ClothItem item = (ClothItem)newBodyPart;
        _aspectsManager.DequipItem(ItemEquiped);
        _aspectsManager.EquipOrUse(newBodyPart);

        Destroy(Part); //Destroys the previous body part.

        ItemEquiped = item;

        Part = Instantiate(item.bodyPartPrefab, transform); //Instatiate the new one.
        Part.transform.localPosition    = Position;
        Part.transform.localScale       = Scale;
    }

    /// <summary>
    /// Removes the current body part, replacing it with the default one.
    /// </summary>
    public void RemoveBodyPart() => ChangeBodyPart(defaultBodyPart);
}