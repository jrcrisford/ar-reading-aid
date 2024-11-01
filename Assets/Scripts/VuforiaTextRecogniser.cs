using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using TMPro;

public class VuforiaTextRecogniser : MonoBehaviour
{
    private string apiKey = "AIzaSyAxQ1eUHUXawHj3Lj3OzfrxE4JpXlzpgp0";  // API key for Google Vision service
    private bool requestSent = false;                                   // Prevents multiple requests from being sent simultaneously
    private Texture2D screenshotTexture;                                // Stores the captured screenshot for processing
    public RectTransform textCaptureRegion;                             // Defines the UI region for text capture
    public TMP_Text resultText;                                         // Displays recognised text on the UI
    public string storedResultText = "";                                // Stores recognised text for use by other scripts

    // When triggered initiates a text capture and recognition
    public void CaptureAndProcessText()
    {
        // Ensures only one request at a time
        if (!requestSent)
        {
            requestSent = true;
            // Starts the screenshot capture and processing
            StartCoroutine(CaptureScreenshotAndSend());
        }
        else
        {
            Debug.Log("Processing... Please wait before sending another request.");
        }
    }

    // Captures the screen area defined by `textCaptureRegion` and prepares it for text recognition
    private IEnumerator CaptureScreenshotAndSend()
    {
        // Waits until the end of the frame to capture
        yield return new WaitForEndOfFrame();

        // Calculate the region of the screen to capture based on `textCaptureRegion` object's position and size
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, textCaptureRegion.position);
        int captureWidth = (int)textCaptureRegion.rect.width;
        int captureHeight = (int)textCaptureRegion.rect.height;
        int startX = (int)(screenPosition.x - (captureWidth / 2));
        int startY = (int)(screenPosition.y - (captureHeight / 2));

        // Create a texture for the selected region and capture its pixels
        screenshotTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(startX, startY, captureWidth, captureHeight), 0, 0);
        screenshotTexture.Apply();

        // Send the captured image to Google Vision for text detection
        StartCoroutine(SendImageToGoogleVision(screenshotTexture));
    }

    // Sends the captured image to the Google Vision API for text recognition
    private IEnumerator SendImageToGoogleVision(Texture2D image)
    {
        // Convert image to PNG format and encode image as Base64 for JSON payload
        byte[] imageBytes = image.EncodeToPNG();
        string base64Image = System.Convert.ToBase64String(imageBytes);

        // Create JSON request for Google Vision API
        string jsonRequest = "{\"requests\": [{\"image\": {\"content\": \"" + base64Image + "\"},\"features\": [{\"type\": \"TEXT_DETECTION\"}]}]}";
        byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

        // Initialise web request with API URL and request data
        string url = $"https://vision.googleapis.com/v1/images:annotate?key={apiKey}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Wait for response from Google Vision API
        yield return request.SendWebRequest();

        // Send response for processing if recieved, announce error if not
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Google Vision Response: " + request.downloadHandler.text);
            ProcessGoogleVisionResponse(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Google Vision Error: " + request.error);
            requestSent = false; // Allow new requests if there was an error
        }
    }

    // Processes the response from Google Vision and extracts the recognised text
    private void ProcessGoogleVisionResponse(string jsonResponse)
    {
        // Deserialize JSON response to extract recognised text
        var response = JsonUtility.FromJson<GoogleVisionResponse>(jsonResponse);

        if (response.responses.Length > 0 && response.responses[0].textAnnotations.Length > 0)
        {
            // Get the main text description and display it on the UI, as well as store it
            string mainText = response.responses[0].textAnnotations[0].description;
            resultText.text = mainText;             // Update tmp Text object with recognised text
            storedResultText = mainText;            // Save recognised text for external access
            Debug.Log("Recognised Text: " + mainText);
        }
        else
        {
            // Display error message and clear stored result if no text was recognised
            resultText.text = "No text found.";     
            storedResultText = "";
            Debug.Log("No text recognised.");
        }

        // Reset flag to allow new requests
        requestSent = false;                        
    }
}

// Classes for deserializing Google Vision response JSON data
[System.Serializable]
public class GoogleVisionResponse
{
    public Response[] responses;
}

[System.Serializable]
public class Response
{
    public TextAnnotation[] textAnnotations;
}

[System.Serializable]
public class TextAnnotation
{
    public string description;
}
