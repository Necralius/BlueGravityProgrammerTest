using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionView : MonoBehaviour
{
    #region - Singleton Pattern -
    public static InteractionView Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private GameObject interactionLabel;

    public void InteractionLabel(bool isOnArea)
    {
        interactionLabel.SetActive(isOnArea);
    }

    public void UpdateInteractionView()
    {

    }




}