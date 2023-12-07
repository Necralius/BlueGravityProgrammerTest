using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class InteractionModel : MonoBehaviour
{
    //Direct Dependencies
    private CircleCollider2D _col => GetComponent<CircleCollider2D>();

    //Interactions Actions
    [SerializeField] private UnityAction _onInteractionStart    = null;
    [SerializeField] private UnityAction _onInteractionEnd      = null;

    [Header("Interaction Settings")]
    [SerializeField] private float _interactionRadius = 2f;


    private void OnValidate()
    {
        _col.isTrigger = true;
        _col.radius = _interactionRadius;
    }
    public void Interact()
    {
        _onInteractionStart.Invoke();

    }
    public void ExitInteraction()
    {
        _onInteractionEnd.Invoke();

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}