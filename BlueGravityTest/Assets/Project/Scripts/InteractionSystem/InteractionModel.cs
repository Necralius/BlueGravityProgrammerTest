using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents an interactable object in the game. (MVC - Model)
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class InteractionModel : MonoBehaviour
{
    // Direct Dependencies
    private CircleCollider2D _col => GetComponent<CircleCollider2D>();

    // Interactions Actions
    [SerializeField] private UnityEvent _onInteractionStart = null;
    [SerializeField] private UnityEvent _onInteractionEnd   = null;
    [SerializeField] private UnityEvent _onButtonPress      = null;

    [Header("Interaction Settings")]
    [SerializeField] private float _interactionRadius = 2f;
    public bool isInteracting = false;

    /// <summary>
    /// Validate and set up the CircleCollider2D properties in the editor.
    /// </summary>
    private void OnValidate()
    {
        _col.isTrigger = true;
        _col.radius = _interactionRadius;
    }

    /// <summary>
    /// Initiates the interaction.
    /// </summary>
    public void Interact()
    {
        isInteracting = true;
        _onInteractionStart?.Invoke();
    }

    /// <summary>
    /// Triggers when the interaction button is pressed while is already in the interaction.
    /// </summary>
    public void PressInteraction() => _onButtonPress?.Invoke(); //Also verifies if the unityEvent is invalid using the '?' symbol.

    /// <summary>
    /// Exits the interaction state.
    /// </summary>
    public void ExitInteraction()
    {
        isInteracting = false;
        _onInteractionEnd?.Invoke();
    }
}
