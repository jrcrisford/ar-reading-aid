using UnityEngine;
using TMPro;

public class TextPageNavigator : MonoBehaviour
{
    private TMP_Text textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
        textMeshPro.pageToDisplay = 1;
    }

    public void NextPage()
    {
        if (textMeshPro.pageToDisplay < textMeshPro.textInfo.pageCount)
        {
            textMeshPro.pageToDisplay++;
        }
    }

    public void PreviousPage()
    {
        if (textMeshPro.pageToDisplay > 1)
        {
            textMeshPro.pageToDisplay--;
        }
    }
}

