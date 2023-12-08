using UnityEngine;

/// <summary>
/// Handles interactions between the player and interactable objects. (MVC - Controller)
/// </summary>
public class InteractionController : MonoBehaviour
{
    private InteractionView _interactionView = null;

    [Header("Interaction Data")]
    [SerializeField] private bool _onInteractionArea = false;
    [SerializeField] private InteractionModel _interactionInArea = null;

    private void Start()
    {
        // Get the InteractionView component in the children objects.
        _interactionView = GetComponentInChildren<InteractionView>();
    }

    /// <summary>
    /// Initiates the interaction with the object in the interaction area.
    /// </summary>
    public void Interact()
    {
        if (_onInteractionArea)
        {
            // Check if there is an interaction model and it's not already interacting.
            if (_interactionInArea != null && !_interactionInArea.isInteracting)
                _interactionInArea.Interact();

            // Check if the interaction model is already interacting and trigger a press interaction.
            if (_interactionInArea.isInteracting)
                _interactionInArea.PressInteraction();
        }
    }

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The Collider2D of the other GameObject.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering GameObject has the "InteractionModel" tag.
        if (other.CompareTag("InteractionModel"))
        {
            _onInteractionArea = true;
            _interactionInArea = other.GetComponent<InteractionModel>();
            _interactionView.InteractionLabel(true);
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger zone.
    /// </summary>
    /// <param name="other">The Collider2D of the other GameObject.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Exit the interaction and reset variables when leaving the interaction area.
        _interactionInArea?.ExitInteraction();
        _onInteractionArea = false;
        _interactionInArea = null;
        _interactionView.InteractionLabel(false);
    }
}