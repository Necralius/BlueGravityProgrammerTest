using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the behavior of a dialogue, including typing text.
/// </summary>
public class DialogueModel : MonoBehaviour
{
    [SerializeField] private List<string>   _phrases        = new List<string>(); // List of dialogue phrases
    [SerializeField] private TextMeshPro    _dialogueText   = null; // Reference to the TextMeshPro component for displaying dialogue
    [SerializeField] private float          _typingSpeed    = 50f; // Speed at which the text is typed

    private int _currentPhraseIndex = 0; // Index of the current displayed phrase
    private bool _isTyping          = false; // Flag indicating whether the text is currently being typed

    private Coroutine _typingCoroutine = null; // Coroutine for typing text

    /// <summary>
    /// Starts the dialogue by activating the dialogue text and typing the first phrase.
    /// </summary>
    public void StartDialogue()
    {
        _dialogueText.gameObject.SetActive(true);
        _typingCoroutine = StartCoroutine(TypeDialogue(_phrases[_currentPhraseIndex]));
    }

    /// <summary>
    /// Closes the dialogue by deactivating the dialogue text.
    /// </summary>
    public void CloseDialogue()
    {
        _dialogueText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Displays the next phrase in the dialogue.
    /// </summary>
    public void NextDialogue()
    {
        if (_isTyping) return;

        _currentPhraseIndex = (_currentPhraseIndex + 1) % _phrases.Count;
        _typingCoroutine = StartCoroutine(TypeDialogue(_phrases[_currentPhraseIndex]));
    }

    /// <summary>
    /// Coroutine for typing each character of the dialogue text.
    /// </summary>
    /// <param name="phrase">The phrase to be typed.</param>
    private IEnumerator TypeDialogue(string phrase)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _isTyping = true;
        _dialogueText.text = "";

        foreach (char letter in phrase)
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(1f / _typingSpeed);
        }

        _isTyping = false;
    }
}
