using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu interactions.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Quits the game when called.
    /// </summary>
    public void Quit() => Application.Quit();

    /// <summary>
    /// Starts the game by loading the game scene (index 1).
    /// </summary>
    public void StartGame() => SceneManager.LoadScene(1);

    /// <summary>
    /// Open the seted url.
    /// </summary>
    public void OpenLinkedIn() => Application.OpenURL("https://www.linkedin.com/in/victor-paulo-dev/");

    /// <summary>
    /// Open the seted url.
    /// </summary>
    public void OpenGithub() => Application.OpenURL("https://github.com/Necralius");

}