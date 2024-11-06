using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections.Generic;

public class DictionaryLookup : MonoBehaviour
{
    // Text component to display the result
    [SerializeField] private TMP_Text resultText;
    // API key for Merriam-Webster Dictionary API
    [SerializeField] private string apiKey = "8d142832-5450-4d2f-9294-152a502e87a3";
    // Base URL for dictionary API
    [SerializeField] private string apiUrl = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
    // Reference to WordSelector component
    private WordSelector wordSelector;

    private void Awake()
    {
        // Initialize WordSelector component
        wordSelector = GetComponent<WordSelector>();
        Debug.Log("DictionaryLookup script initialised. WordSelector component retrieved.");
    }

    public void OnLookupButtonPressed()
    {
        // Retrieve selected word from WordSelector
        string selectedWord = wordSelector.GetSelectedWord();
        Debug.Log($"Button pressed. Selected word: {selectedWord}");

        // Check if a word is selected, then start fetching its definition
        if (!string.IsNullOrEmpty(selectedWord))
        {
            Debug.Log("Starting coroutine to fetch definition.");
            StartCoroutine(FetchDefinition(selectedWord));
        }
        else
        {
            // Display message if no word is selected
            Debug.LogWarning("No word selected.");
            resultText.text = "No word selected.";
        }
    }

    private IEnumerator FetchDefinition(string word)
    {
        // Construct the API request URL
        string requestUrl = $"{apiUrl}{word}?key={apiKey}";
        Debug.Log($"Fetching definition from URL: {requestUrl}");

        // Send a GET request to the dictionary API
        using (UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl))
        {
            // Wait for response
            yield return webRequest.SendWebRequest();

            // Check for connection or protocol errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error fetching definition: {webRequest.error}");
                resultText.text = $"Error: {webRequest.error}";
            }
            else
            {
                Debug.Log("Definition fetched successfully. Processing response.");
                ProcessResponse(webRequest.downloadHandler.text);   // Process the JSON response if successful
            }
        }
    }

    private void ProcessResponse(string jsonResponse)
    {
        Debug.Log("Processing JSON response.");
        Debug.Log($"Raw JSON response: {jsonResponse}");

        // Deserialize the JSON response to get definitions
        List<MerriamWebsterResponse> definitions = JsonUtility.FromJson<Wrapper>($"{{\"items\":{jsonResponse}}}").items;

        // Check if definitions exist and display them
        if (definitions != null && definitions.Count > 0 && definitions[0].shortdef.Length > 0)
        {
            // Concatenate all definitions and display in resultText
            string allDefinitions = string.Join("\n", definitions[0].shortdef);
            Debug.Log($"Definition found: {allDefinitions}");
            resultText.text = allDefinitions;
        }
        else
        {
            // Display message if no definitions are found
            Debug.LogWarning("Definition not found in the response.");
            resultText.text = "Definition not found.";
        }
    }

}

// Wrapper class for deserializing the JSON response into a list
[System.Serializable]
public class Wrapper
{
    public List<MerriamWebsterResponse> items;
}

// Class representing the structure of each dictionary entry
[System.Serializable]
public class MerriamWebsterResponse
{
    public string[] shortdef;   // Array of short definitions
}
