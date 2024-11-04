using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using System;

public class TextToSpeech : MonoBehaviour
{
    private string apiKey = "AIzaSyDbtpSgtalVoaxPfqXcED-VByff07WKiro";      // API key for accessing Google Text-to-Speech service
    public AudioSource audioSource;                                         // Reference to the AudioSource component to play audio
    public TextRecogniser textRecogniser;                            // Reference to a VuforiaTextRecogniser script that stores recognised text

    // Method to convert recognised text to speech using Google Text-to-Speech
    public void ConvertTextToSpeech()
    {
        // Get the stored text from the VuforiaTextRecogniser
        string text = textRecogniser.storedResultText;

        // Check if there’s any text to convert
        if (!string.IsNullOrEmpty(text))
            StartCoroutine(SendTextToGoogleTTS(text)); // Send the text to Google API for conversion
        else
            Debug.LogWarning("No text provided for speech synthesis."); 
    }

    // Coroutine resposible for sending a request to Google Text-to-Speech API
    private IEnumerator SendTextToGoogleTTS(string text)
    {
        // Prepare JSON request with the specified text, language, and audio format
        string jsonRequest = 
        "{\"input\": {\"text\": \"" + text + "\"}," +
        "\"voice\": {\"languageCode\": \"en-US\", \"ssmlGender\": \"NEUTRAL\"}," +
        "\"audioConfig\": {\"audioEncoding\": \"LINEAR16\"}}";

        // Convert JSON request to bytes for sending
        byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

        // Set up the request URL with the API key
        string url = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={apiKey}";

        // Create a new UnityWebRequest for sending the data to Google Text-to-Speech
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(requestData);      // Send the JSON data
        request.downloadHandler = new DownloadHandlerBuffer();          // Receive the response data
        request.SetRequestHeader("Content-Type", "application/json");   // Set content type as JSON

        // Wait for the request to complete
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result == UnityWebRequest.Result.Success) 
        {
            Debug.Log("Google TTS Response: " + request.downloadHandler.text);
            ProcessGoogleTTSResponse(request.downloadHandler.text);     // Process the response
        }
        else
            Debug.LogError("Google TTS Error: " + request.error);
    }

    // Process the response from Google TTS to play the audio
    private void ProcessGoogleTTSResponse(string jsonResponse)
    {
        // Deserialize JSON response to get audio content in Base64 format
        var response = JsonUtility.FromJson<GoogleTTSResponse>(jsonResponse);

        // Check if audio content is present in the response
        if (string.IsNullOrEmpty(response.audioContent))
        {
            Debug.LogError("No audio content received.");
            return;
        }
        try
        {
            // Decode the Base64 string to get raw audio data
            byte[] audioData = System.Convert.FromBase64String(response.audioContent);

            // Convert the audio data to an AudioClip and play it
            AudioClip clip = WavConverter.ToAudioClip(audioData, "GoogleTTSAudio");
            if (clip == null)
            {
                Debug.LogError("Audio clip creation failed.");
                return;
            }
            
            // Assign the audio clip to the AudioSource and play it
            audioSource.clip = clip;
            audioSource.Play();
        }
        catch (Exception e)
        {
            Debug.LogError("Error processing audio: " + e.Message);
        }
    }
}

// Class to deserialize JSON response from Google TTS
[System.Serializable]
public class GoogleTTSResponse
{
    public string audioContent;
}
