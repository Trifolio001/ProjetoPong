using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionOptionsButtonsTouggle : MonoBehaviour
{
    public Text TextReference;
    public GameObject paddleReference1;
    public GameObject paddleReference2;
    public GameObject Dificulty;
    public GameObject TextName;
    public Toggle toggleReference;
    //0 = player, 1 = IA Facil, 2 = IA medio, 3 = IA dificil, 4 - muito dificel 
    public int SelectTipe = 0;
    public bool isColorPlayer = false;

    public void OnToggleClick()
    {
        if (toggleReference.isOn)
        {
            TextReference.text = "Player";
            SelectTipe = 0;
            paddleReference1.SetActive(true);
            paddleReference2.SetActive(false);
            Dificulty.SetActive(false);
            TextName.SetActive(true);
        }
        else
        {
            TextReference.text = "Bot";
            SelectTipe = 1;
            paddleReference1.SetActive(false);
            paddleReference2.SetActive(true);
            Dificulty.SetActive(true);
            TextName.SetActive(false);
        }

        if (isColorPlayer)
        {
            SaveController.Instance.IdentyPlayer = SelectTipe;
        }
        else
        {
            SaveController.Instance.IdentyEnemy = SelectTipe;
        }
    }
}
