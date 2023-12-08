using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private InteractionView _interactionView = null;

    [SerializeField] private bool               _onInteractionArea = false;
    [SerializeField] private InteractionModel   _interactionInArea = null;

    private void Start()
    {
        _interactionView = GetComponentInChildren<InteractionView>();
    }

    public void Interact()
    {
        if (_onInteractionArea)
            _interactionInArea.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<InteractionModel>())
        {
            _onInteractionArea = true;
            _interactionInArea = other.GetComponent<InteractionModel>();
            _interactionView.InteractionLabel(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _interactionInArea.ExitInteraction();
        _onInteractionArea = false;
        _interactionInArea = null;
        _interactionView.InteractionLabel(false);
    }


}