using UnityEngine;
using NekraByte;

public class BodyPart : MonoBehaviour
{
    private CharacterAspects _aspectsManager;

    [SerializeField] private GameObject     Part        = null;
    [SerializeField] private EquipableItem  ItemEquiped = null;

    public  ClothType    Type        = ClothType.Suit;
    private Vector3      Position    = Vector3.zero;
    private Vector3      Scale       = Vector3.zero;
    private bool         Initialized = false;

    [SerializeField] private EquipableItem defaultBodyPart = null;

    public BodyPart(GameObject bodyPart, ClothType Type)
    {
        this.Type   = Type;
        Part        = bodyPart;
        Position    = bodyPart.transform.localPosition;
        Scale       = bodyPart.transform.localScale;
        Initialized = true;
    }

    private void Start()
    {
        _aspectsManager = GetComponentInParent<CharacterAspects>();
        if (!Initialized)
        {
            if (Part != null && ItemEquiped != null)
            {
                _aspectsManager.EquipArmor(ItemEquiped);
                Position    = Part.transform.localPosition;
                Scale       = Part.transform.localScale;
                Initialized = true;
            }
        }
    }

    public void ChangeBodyPart(EquipableItem newBodyPart)
    {
        if (newBodyPart == null)
        {
            Debug.LogWarning("Invalid body part!");
            return;
        }

        _aspectsManager.DequipArmor(ItemEquiped);
        _aspectsManager.EquipArmor(newBodyPart);

        Destroy(Part);

        ItemEquiped = newBodyPart;

        Part = Instantiate(newBodyPart.bodyPartPrefab, transform);
        Part.transform.localPosition    = Position;
        Part.transform.localScale       = Scale;
    }

    public void RemoveBodyPart()
    {
        ChangeBodyPart(defaultBodyPart);
    }
}