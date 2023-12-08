using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages overall game behavior, including pause, inventory, and death interactions.
/// </summary>
public class GameController : MonoBehaviour
{
    private ControllerTopDown _playerInstance = null;

    [Header("Inventory and Pause Interactions")]
    public GameObject inventoryObject   = null;
    public GameObject pauseMenu         = null;
    public GameObject deathScreen       = null;

    public bool isPaused        = false;
    public bool inventoryOpen   = false;

    private void Start()
    {
        // Get the player instance associated with this GameController
        _playerInstance = GetComponentInParent<ControllerTopDown>();
    }

    /// <summary>
    /// Toggles the pause menu on and off.
    /// </summary>
    public void PauseInteraction()
    {
        if (pauseMenu == null) return;

        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        isPaused = !isPaused;
    }

    /// <summary>
    /// Toggles the inventory on and off.
    /// </summary>
    public void InventoryInteraction()
    {
        if (inventoryObject == null) return;

        inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
        inventoryOpen = !inventoryOpen;

        // If opening the inventory, update the UI
        if (inventoryObject.activeInHierarchy) _playerInstance.aspects.UpdateUI(true);
    }

    /// <summary>
    /// Forces the inventory to a specific state (open or closed).
    /// </summary>
    /// <param name="forceState">The desired state of the inventory.</param>
    public void InventoryInteraction(bool forceState)
    {
        if (inventoryObject == null) return;

        inventoryObject.SetActive(forceState);
        inventoryOpen = forceState;

        // If opening the inventory, update the UI
        if (forceState) _playerInstance.aspects.UpdateUI(true);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame() => Application.Quit();

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void ReturnToMenu() => SceneManager.LoadScene(0); // Load menu scene

    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    /// <summary>
    /// Displays the death screen.
    /// </summary>
    public void OnDeath() => deathScreen.SetActive(true);
}