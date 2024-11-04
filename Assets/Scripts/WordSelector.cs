using UnityEngine;
using TMPro;

public class WordSelector : MonoBehaviour
{
    public TMP_Text textMeshPro;            // Reference to the TMP object
    private string originalText;            // To store original text for reset
    private int currentWordIndex = -1;      // Current highlighted word index
    private bool isTextUpdated = false;     // Flag to track text updates

    void Start()
    {
        StoreOriginalText();  // Store initial text
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;

                // Check for word at the touch position
                int wordIndex = TMP_TextUtilities.FindIntersectingWord(textMeshPro, touchPosition, null);
                if (wordIndex != -1)
                {
                    HighlightWord(wordIndex);
                }
            }
        }

        // Check if the text has been updated dynamically
        if (isTextUpdated)
        {
            StoreOriginalText();
            isTextUpdated = false;
        }
    }

    // Method to store original text
    private void StoreOriginalText()
    {
        originalText = textMeshPro.text;
    }

    // Highlight the word at the specified index
    private void HighlightWord(int wordIndex)
    {
        // If the same word is selected, do nothing
        if (wordIndex == currentWordIndex) return;

        // Reset the TMP text to original
        textMeshPro.text = originalText;

        // Update current word index
        currentWordIndex = wordIndex;

        TMP_WordInfo wordInfo = textMeshPro.textInfo.wordInfo[wordIndex];
        string highlightedText = originalText.Substring(0, wordInfo.firstCharacterIndex)
                               + "<mark=#FF00FF50><color=#FFFFFF>"
                               + wordInfo.GetWord()
                               + "</color></mark>"
                               + originalText.Substring(wordInfo.lastCharacterIndex + 1);

        // Set the text with the new highlight
        textMeshPro.text = highlightedText;
    }

    // Public method to flag text as updated (call this from VuforiaTextRecogniser)
    public void OnTextUpdated()
    {
        isTextUpdated = true;
    }
}
