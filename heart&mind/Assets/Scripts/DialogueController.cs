using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // For UI elements

public class DisplayTextOnTrigger : MonoBehaviour
{
    private Text uiText;  // Reference to the UI Text
    public Font catFont;  // Font for cat
    public Font wolfFont; // Font for wolf
    public Color catColor = Color.white;  // Color of the text for cat
    public Color wolfColor = Color.white;  // Color of the text for wolf
    public float letterDelay = 0.05f;  // Delay between each letter
    public float sectionDelay = 1f;  // Delay between text sections

    public string[] textSections;  // Array of text sections to display
    public string[] characterSpeaker; //Who is saying a certain text section
    //private int currentSectionIndex = 0;

    private void Start()
    {

        uiText = GameObject.FindWithTag("Dialogue Box").GetComponent<Text>(); //Assigns the GameObjext
        uiText.text = "";  // Ensure the UI text starts empty

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the collider is the player
        {
            StartCoroutine(ShowTextSequence());
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator ShowTextSequence()
    {
        // Loop through all text sections
        for (int i = 0; i < textSections.Length; i++)
        {

            Debug.Log("Section is " + i + " and character is " + characterSpeaker[i]);
            if (characterSpeaker[i] == "Cat") //assigns the cat variables
            {
                uiText.font = catFont;
                uiText.color = catColor;
                Debug.Log("Dialogue " + i + " is Cat");
            }
            else  //assigns the wolf variables
            {
                uiText.font = wolfFont;
                uiText.color = wolfColor;
                Debug.Log("Dialogue " + i + " is Wolf");
            }

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