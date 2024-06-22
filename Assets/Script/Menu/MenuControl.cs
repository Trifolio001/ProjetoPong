using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuControl : MonoBehaviour
{
    [Header("Referencias das telas")]
    public Image Logo;
    public GameObject Principal; 
    public GameObject OpcoesPlayer;

    [Header("Referencias OpcoesPlayer")]

    public Button uiButton1;
    public Button uiButton2;
    public Image paddleReferenceColor1e1;
    public Image paddleReferenceColor1e2;
    public Image paddleReferenceColor2e1;
    public Image paddleReferenceColor2e2;
    public GameObject Player1Options;
    public GameObject Player2Options;
    public GameObject Player3Options;
    public GameObject EstatisticOptions;
    public GameObject LogoObject;
    public GameObject TextEstatistc;
    public Animator AnimatorLogo;
    public Animator AnimatorText;

    public SelectionOptionsButtonsTouggle toggleReference1;
    public SelectionOptionsButtonsTouggle toggleReference2;
    private bool ActionMove = false;
    private bool ActionMoveExit = false;
    private Vector3 PosMove = new Vector3(0, 0, 0);
    private Vector3 PosMove1 = new Vector3(0, 0, 0);
    public int velocity = 500;

    public TextMeshProUGUI uiWinner;
    public TextMeshProUGUI uiStatistc;

    // Start is called before the first frame update
    void Start()
    {
        LogoObject.SetActive(true);
        Principal.SetActive(true);
        OpcoesPlayer.SetActive(true);
        EstatisticOptions.SetActive(false);
        ActionMove = false;
        ActionMoveExit = false;
        Player1Options.transform.localPosition = new Vector3(-500, 0, 0);
        Player2Options.transform.localPosition = new Vector3(500, 0, 0);
        Player3Options.transform.localPosition = new Vector3(0, -500, 0);
        Logo.rectTransform.localScale = new Vector2(1,1);
                
        string lastWinner = SaveController.Instance.GetLastWinner();
        if (lastWinner != "")
            uiWinner.text = "Último vencedor: " + lastWinner;
        else
            uiWinner.text = "";

        string lastStatistc = SaveController.Instance.GetLastStatistc();
        if (lastStatistc != "")
            uiStatistc.text = lastStatistc;
        else
            uiStatistc.text = "Nenhuma Informação";
    }

    public void OptionsPlayer()
    {
        Principal.SetActive(false);
        OpcoesPlayer.SetActive(true);
        toggleReference1.OnToggleClick();
        toggleReference2.OnToggleClick();
        paddleReferenceColor1e1.color = uiButton1.colors.normalColor;
        paddleReferenceColor1e2.color = uiButton1.colors.normalColor;
        paddleReferenceColor2e1.color = uiButton2.colors.normalColor;
        paddleReferenceColor2e2.color = uiButton2.colors.normalColor;
        SaveController.Instance.colorEnemy = uiButton1.colors.normalColor;
        SaveController.Instance.colorPlayer = uiButton2.colors.normalColor;
        playOptions();
    }

    void Update()
    {
        Debug.Log(ActionMove);
        if (ActionMove == true)
        {

            Player1Options.transform.Translate(PosMove * Time.deltaTime);
            Player2Options.transform.Translate(-PosMove * Time.deltaTime);
            Player3Options.transform.Translate(PosMove1 * Time.deltaTime);

            if (((Player1Options.transform.localPosition.x >= -10) && (Player1Options.transform.localPosition.x <= 10))) //&& ((ObjectScreen.transform.localPosition.y >= -(10 - 1)) && (ObjectScreen.transform.localPosition.y <= (10 - 1))))
            {
                Player1Options.transform.localPosition = new Vector3(0, 0, 0);
                Player2Options.transform.localPosition = new Vector3(0, 0, 0);
                Player3Options.transform.localPosition = new Vector3(0, 0, 0);
                ActionMove = false;
            }
        }
        if (ActionMoveExit == true)
        {
            Player1Options.transform.Translate(PosMove * Time.deltaTime);
            Player2Options.transform.Translate(-PosMove * Time.deltaTime);
            Player3Options.transform.Translate(PosMove1 * Time.deltaTime);
            if (((Player1Options.transform.localPosition.x > 500) || (Player1Options.transform.localPosition.x < -500))) //|| ((ObjectScreen.transform.localPosition.y > 1500) || (ObjectScreen.transform.localPosition.y < -1500)))
            {
                //Player1Options.transform.localPosition = new Vector3(DirNumX, DirNumY, 0);
                ActionMoveExit = false;
                Principal.SetActive(true);
            }
        }
    }

    public void playOptions()
    {
        ActionMove = true;   
        PosMove = new Vector3(velocity, 0, 0);
        PosMove1 = new Vector3( 0, velocity, 0);
        AnimatorLogo.SetTrigger("EntraceMove");
        TextEstatistc.SetActive(false);
    }


    public void playExitOptions()
    {
        ActionMoveExit = true;
        PosMove = new Vector3(-velocity, 0, 0);
        PosMove1 = new Vector3(0, -velocity, 0);
        AnimatorLogo.SetTrigger("ExitMove");
        TextEstatistc.SetActive(true);
    }

    public void playEstatistic()
    {
        EstatisticOptions.SetActive(true);
        AnimatorText.SetTrigger("EntraceMove");
        Principal.SetActive(false);
    }

    public void ExitEstatistic()
    {

        EstatisticOptions.SetActive(false);
        AnimatorText.SetTrigger("ExitMove");
        Principal.SetActive(true);
    }
}
