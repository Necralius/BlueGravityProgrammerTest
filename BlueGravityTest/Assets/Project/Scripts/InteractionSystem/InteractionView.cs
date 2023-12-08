using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionView : MonoBehaviour
{
    [SerializeField] private GameObject interactionLabel;

    public void InteractionLabel(bool isOnArea)
    {
        interactionLabel.SetActive(isOnArea);
    }
}