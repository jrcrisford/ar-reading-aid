using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI mainText;
    public TMP_FontAsset openDyslexic;
    public TMP_FontAsset lexieReadable;
    public TMP_FontAsset tiresias;
    public TMP_FontAsset arial;
    private int maxFontSize = 60;
    private int maxCharSpacing = 20;
    private int maxLineSpacing = 100;
    Color orange = new Color(1.0f, 0.64f, 0.0f); //https://discussions.unity.com/t/orange-color-in-scripts/35822

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInputData(int fontInt)
    {
        switch (fontInt)
        {
            case 0:
                if (mainText != null)
                {
                    mainText.font = arial;
                }
                break;

            case 1:
                if (mainText != null)
                {
                    mainText.font = openDyslexic;
                }
                break;

            case 2:
                if (mainText != null)
                {
                    mainText.font = lexieReadable;
                }
                break;

            case 3:
                if (mainText != null)
                {
                    mainText.font = tiresias;
                }
                break;
        }
    }

    public void ChangeTextColor(string color)
    {
        switch (color)
        {
            case "white":
                if (mainText != null)
                {
                    mainText.color = Color.white;
                }
                break;

            case "black":
                if (mainText != null)
                {
                    mainText.color = Color.black;
                }
                break;

            case "yellow":
                if (mainText != null)
                {
                    mainText.color = Color.yellow;
                }
                break;

            case "green":
                if (mainText != null)
                {
                    mainText.color = Color.green;
                }
                break;

            case "orange":
                if (mainText != null)
                {
                    mainText.color = orange;
                }
                break;
        }
    }

    public void SetFontSize(float size)
    {
        if (mainText != null)
        {
            int fontSize = (int)(size * maxFontSize);

            mainText.fontSize = fontSize;
        }
    }

    public void SetCharacterSpacing(float spacing)
    {
        if (mainText != null)
        {
            int characterSpacing = (int)(spacing * maxCharSpacing);

            mainText.characterSpacing = characterSpacing;
        }
    }

    public void SetLineSpacing(float spacing)
    {
        if (mainText != null)
        {
            int lineSpacing = (int)(spacing * maxLineSpacing);

            mainText.lineSpacing = lineSpacing;
        }
    }    
}