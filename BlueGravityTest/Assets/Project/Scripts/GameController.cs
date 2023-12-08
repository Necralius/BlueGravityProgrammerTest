using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ControllerTopDown _playerInstance;

    [Header("Inventory and Pause Interactions")]
    public GameObject inventoryObject   = null;
    public GameObject pauseMenu         = null;

    public bool isPaused        = false;
    public bool inventoryOpen   = false;

    private void Start()
    {
        _playerInstance = GetComponentInParent<ControllerTopDown>();
    }

    public void PauseInteraction()
    {
        if (pauseMenu == null) return;

        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        isPaused = !isPaused;
    }

    public void InventoryInteraction()
    {
        if (inventoryObject == null) return;

        inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
        inventoryOpen = !inventoryOpen;

        if (inventoryObject.activeInHierarchy) _playerInstance.aspects.UpdateUI(true);
    }

    public void InventoryInteraction(bool forceState)
    {
        if (inventoryObject == null) return;

        inventoryObject.SetActive(forceState);
        inventoryOpen = forceState;
        if (forceState) _playerInstance.aspects.UpdateUI(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        //Load Menu
    }
}