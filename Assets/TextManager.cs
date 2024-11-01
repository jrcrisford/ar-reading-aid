using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI mainText;
    public TMP_FontAsset openDyslexic;
    public TMP_FontAsset lexieReadable;
    public TMP_FontAsset tiresias;
    public TMP_FontAsset arial;

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
                break;

            case 1:
                mainText.font = openDyslexic;
                break;

            case 2:
                mainText.font = lexieReadable;
                break;

            case 3:
                mainText.font = tiresias;
                break;

            case 4:
                mainText.font = arial;
                break;
        }
    }
}