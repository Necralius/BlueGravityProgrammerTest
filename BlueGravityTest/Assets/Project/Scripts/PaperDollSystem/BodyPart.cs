using UnityEngine;
using NekraByte;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private GameObject Part = null;

    public  ClothType    Type        = ClothType.Suit;
    private Vector3      Position    = Vector3.zero;
    private Vector3      Scale       = Vector3.zero;
    private bool initialized = false;

    public BodyPart(GameObject bodyPart, ClothType Type)
    {
        this.Type   = Type;
        Part        = bodyPart;
        Position    = bodyPart.transform.localPosition;
        Scale       = bodyPart.transform.localScale;
        initialized = true;
    }

    private void Start()
    {
        if (!initialized)
        {
            if (Part != null)
            {
                Position    = Part.transform.localPosition;
                Scale       = Part.transform.localScale;
                initialized = true;
            }
        }
    }

    public void ChangeBodyPart(GameObject newBodyPart)
    {
        Destroy(Part);

        Part = Instantiate(newBodyPart, transform);
        Part.transform.localPosition    = Position;
        Part.transform.localScale       = Scale;
    }
}