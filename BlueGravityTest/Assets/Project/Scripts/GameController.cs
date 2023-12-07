using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region - Singleton Pattern -
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public InventoryController _inventoryController = null;

    [Header("Inventory and Pause Interactions")]
    public GameObject inventoryObject   = null;
    public GameObject pauseMenu         = null;

    public bool isPaused        = false;
    public bool inventoryOpen   = false;

    private void Start()
    {
        _inventoryController = inventoryObject.GetComponent<InventoryController>();
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