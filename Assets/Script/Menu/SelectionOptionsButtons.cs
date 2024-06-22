using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionOptionsButtons : MonoBehaviour
{
    public Button uiButton;
    public Image paddleReferenceColor1;
    public Image paddleReferenceColor2;
    public bool isColorPlayer = false;

    public void OnButtonClick()
    {
        paddleReferenceColor1.color = uiButton.colors.normalColor;
        paddleReferenceColor2.color = uiButton.colors.normalColor;

        if (isColorPlayer)
        {
            SaveController.Instance.colorPlayer = paddleReferenceColor1.color;
        }
        else
        {
            SaveController.Instance.colorEnemy = paddleReferenceColor1.color;
        }
    }

}
