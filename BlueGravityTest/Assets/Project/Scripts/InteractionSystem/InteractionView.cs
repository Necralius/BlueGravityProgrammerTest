using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the visibility of an interaction label in the game. (MVC - View)
/// </summary>
public class InteractionView : MonoBehaviour
{
    [SerializeField] private GameObject interactionLabel;

    /// <summary>
    /// Sets the visibility of the interaction label.
    /// </summary>
    /// <param name="isOnArea">True if the player is in the interaction area, false otherwise.</param>
    public void InteractionLabel(bool isOnArea) => interactionLabel.SetActive(isOnArea);
}