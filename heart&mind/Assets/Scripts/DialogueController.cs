using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // For UI elements

public class DisplayTextOnTrigger : MonoBehaviour
{
    public Text uiText;  // Reference to the UI Text
    public Font newFont;  // Font you want to apply
    public Color textColor = Color.white;  // Color of the text
    public float letterDelay = 0.05f;  // Delay between each letter
    public float sectionDelay = 1f;  // Delay between text sections

    public string[] textSections;  // Array of text sections to display
    private int currentSectionIndex = 0;

    private void Start()
    {
        // Initialize UI Text settings
        uiText.font = newFont;
        uiText.color = textColor;
        uiText.text = "";  // Ensure the UI text starts empty
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the collider is the player
        {
            StartCoroutine(ShowTextSequence());
        }
    }

    private IEnumerator ShowTextSequence()
    {
        // Loop through all text sections
        for (int i = 0; i < textSections.Length; i++)
        {
            yield return StartCoroutine(TypeText(textSections[i]));  // Type out the section
            yield return new WaitForSeconds(sectionDelay);  // Wait before the next section
        }

        // Destroy the GameObject after all sections are shown
        Destroy(gameObject);
    }

    private IEnumerator TypeText(string text)
    {
        uiText.text = "";  // Clear the text before typing

        foreach (char letter in text.ToCharArray())
        {
            uiText.text += letter;  // Add each letter one by one
            yield return new WaitForSeconds(letterDelay);  // Wait for the delay
        }
    }
}