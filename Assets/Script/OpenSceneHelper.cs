using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OpenSceneHelper : MonoBehaviour
{
    public string sceneToOpen;

    public Slider SliderReference1;
    public Slider SliderReference2;
    public TMP_InputField Text1;
    public TMP_InputField Text2;

    public void OpenScene()
    {
        if (SaveController.Instance.IdentyPlayer != 0)
        {
            SaveController.Instance.IdentyPlayer = Mathf.FloorToInt(SliderReference1.value);
            SaveController.Instance.namePlayer = "BOT DIREITO";
        }
        else
        {
            if (Text2.text == "")
            {
                SaveController.Instance.namePlayer = "PlayerDireita";
            }
        }
        if (SaveController.Instance.IdentyEnemy != 0)
        {
            SaveController.Instance.IdentyEnemy = Mathf.FloorToInt(SliderReference2.value);
            SaveController.Instance.nameEnemy = "BOT ESQUERDO";
        }
        else    
        { 
            if (Text1.text == "")
            {
                SaveController.Instance.nameEnemy = "PlayerEsquerda";
            }
        }

        SceneManager.LoadScene(sceneToOpen);
    }
}
