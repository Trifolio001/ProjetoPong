using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputPoint : MonoBehaviour
{
    public Slider inputField;
    public TMP_Text ValuePoints;
    public int value = 1;
    private void Start()
    {
        inputField.value = 3;
        confirmValue(3);
    }

    public void UpdatePoints()
    {
        confirmValue(Mathf.RoundToInt(inputField.value));
    }

    public void confirmValue(int isvalue)
    {
        ValuePoints.text = " pontos máximos";
        switch (isvalue)
        {
            case 0:
                ValuePoints.text = "1" + ValuePoints.text;
                value = 1;
                break;
            case 1:
                ValuePoints.text = "3" + ValuePoints.text;
                value = 3;
                break;
            case 2:
                ValuePoints.text = "5" + ValuePoints.text;
                value = 5;
                break;
            case 3:
                ValuePoints.text = "10" + ValuePoints.text;
                value = 10;
                break;
            case 4:
                ValuePoints.text = "15" + ValuePoints.text;
                value = 15;
                break;
            case 5:
                ValuePoints.text = "25" + ValuePoints.text;
                value = 25;
                break;
            case 6:
                ValuePoints.text = "50" + ValuePoints.text;
                value = 50;
                break;
        }
        SaveController.Instance.Points = value;
    }
}
