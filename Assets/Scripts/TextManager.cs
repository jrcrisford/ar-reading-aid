using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI mainText;
    public TextMeshProUGUI exampleText;
    public TMP_FontAsset openDyslexic;
    public TMP_FontAsset lexieReadable;
    public TMP_FontAsset tiresias;
    public TMP_FontAsset arial;
    private int maxFontSize = 80;
    private int maxCharSpacing = 20;
    private int maxLineSpacing = 100;
    Color orange = new Color(1.0f, 0.64f, 0.0f); //https://discussions.unity.com/t/orange-color-in-scripts/35822

    void Start()
    {
        float initialSliderValue = 0.5f;

        SetFontSize(initialSliderValue);
        SetCharacterSpacing(initialSliderValue);
        SetLineSpacing(initialSliderValue);
    }

    public void HandleInputData(int fontInt)
    {
        switch (fontInt)
        {
            case 0:
                if (mainText && exampleText != null)
                {
                    mainText.font = arial;
                    exampleText.font = arial;
                }
                break;

            case 1:
                if (mainText && exampleText != null)
                {
                    mainText.font = openDyslexic;
                    exampleText.font = openDyslexic;
                }
                break;

            case 2:
                if (mainText && exampleText != null)
                {
                    mainText.font = lexieReadable;
                    exampleText.font = lexieReadable;
                }
                break;

            case 3:
                if (mainText && exampleText != null)
                {
                    mainText.font = tiresias;
                    exampleText.font = tiresias;
                }
                break;
        }
    }

    public void ChangeTextColor(string color)
    {
        switch (color)
        {
            case "white":
                if (mainText && exampleText != null)
                {
                    mainText.color = Color.white;
                    exampleText.color = Color.white;
                }
                break;

            case "black":
                if (mainText && exampleText != null)
                {
                    mainText.color = Color.black;
                    exampleText.color = Color.black;
                }
                break;

            case "yellow":
                if (mainText && exampleText != null)
                {
                    mainText.color = Color.yellow;
                    exampleText.color = Color.yellow;
                }
                break;

            case "green":
                if (mainText && exampleText != null)
                {
                    mainText.color = Color.green;
                    exampleText.color = Color.green;
                }
                break;

            case "orange":
                if (mainText && exampleText != null)
                {
                    mainText.color = orange;
                    exampleText.color = orange;
                }
                break;
        }
    }

    public void SetFontSize(float size)
    {
        if (mainText && exampleText != null)
        {
            int fontSize = (int)(size * maxFontSize);

            mainText.fontSize = fontSize;
            exampleText.fontSize = fontSize;
        }
    }

    public void SetCharacterSpacing(float spacing)
    {
        if (mainText && exampleText != null)
        {
            int characterSpacing = (int)(spacing * maxCharSpacing);

            mainText.characterSpacing = characterSpacing;
            exampleText.characterSpacing = characterSpacing;
        }
    }

    public void SetLineSpacing(float spacing)
    {
        if (mainText && exampleText != null)
        {
            int lineSpacing = (int)(spacing * maxLineSpacing);

            mainText.lineSpacing = lineSpacing;
            exampleText.lineSpacing = lineSpacing;
        }
    }    
}