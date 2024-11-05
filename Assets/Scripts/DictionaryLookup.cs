using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class DictionaryLookup : MonoBehaviour
{
    public WordSelector wordSelector;   // Reference to the WordSelector script
    public TMP_Text definitionText;     // TMP Text to display the definition
    private string apiKey = "8d142832-5450-4d2f-9294-152a502e87a3";
    private string apiUrl = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";

    // Called when the dictionary button is pressed
    public void OnDictionaryButtonPressed()
    {
        string selectedWord = wordSelector.GetSelectedWord();
        if (!string.IsNullOrEmpty(selectedWord))
        {
            StartCoroutine(LookupWord(selectedWord));
        }
        else
        {
            definitionText.text = "Please select a word first.";
        }
    }

    private IEnumerator LookupWord(string word)
    {
        string requestUrl = $"{apiUrl}{word}?key={apiKey}";
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Process the response to extract the definition
            string jsonResponse = request.downloadHandler.text;
            ProcessDictionaryResponse(jsonResponse);
        }
        else
        {
            definitionText.text = "Failed to retrieve definition. Please try again.";
        }
    }

    private void ProcessDictionaryResponse(string jsonResponse)
    {
        // Parse the JSON response as an array
        MerriamWebsterResponse[] definitions = JsonUtility.FromJson<MerriamWebsterResponseArrayWrapper>($"{{\"items\":{jsonResponse}}}").items;

        // Initialize a list to hold all definitions
        List<string> allDefinitions = new List<string>();

        // Iterate through each JSON entry to parse and gather definitions
        foreach (var definition in definitions)
        {
            if (definition != null && definition.shortdef.Length > 0)
            {
                // Concatenate definitions from this entry
                string combinedDefinitions = string.Join("; ", definition.shortdef);
                allDefinitions.Add(combinedDefinitions);
            }
        }

        if (allDefinitions.Count > 0)
        {
            // Display all collected definitions
            definitionText.text = "Definitions: " + string.Join("\n\n", allDefinitions);

            // Log all definitions to the console
            Debug.Log("Retrieved Definitions: " + string.Join("; ", allDefinitions));
        }
        else
        {
            definitionText.text = "No definitions found.";
            Debug.Log("No definitions found.");
        }
    }

    // Wrapper class for deserializing JSON arrays with JsonUtility
    [System.Serializable]
    private class MerriamWebsterResponseArrayWrapper
    {
        public MerriamWebsterResponse[] items;
    }
}



// Classes to parse Merriam-Webster API response
[System.Serializable]
public class MerriamWebsterResponse
{
    public string meta;
    public string[] shortdef;
}
