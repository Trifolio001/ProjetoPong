using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // No script do GameManager
    public Transform playerPaddle;
    public Transform enemyPaddle;
    public TextMeshProUGUI textEndGame;
    public PlayerPaddleControler OptionEnimyPaddle;
    public PlayerPaddleControler OptionPlayerPaddle;
    public BallController ballController;
    public GameObject Menu;
    public GameObject screenEndGame;

    public int playerScore = 0;
    public int enemyScore = 0;
    public TextMeshProUGUI textPointsPlayer;
    public TextMeshProUGUI textPointsEnemy;
    public TMP_Text TextNumPlayer;
    public Rigidbody2D ball;

    public int winPoints = 5;

    // ...
    void Start()
    {
        winPoints = SaveController.Instance.Points;
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        //verifica a tecla esc e dispara a pausa do jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Menu.activeSelf)
            {
                Menu.SetActive(true);
                ball.simulated = (false);
                OptionEnimyPaddle.ConditionPause(true);
                OptionPlayerPaddle.ConditionPause(true);
            }
            else
            {
                Menu.SetActive(false);
                ball.simulated = (true);
                OptionEnimyPaddle.ConditionPause(false);
                OptionPlayerPaddle.ConditionPause(false);
            }
        }
    }

    public void ResetGame()
    {

        Menu.SetActive(false);
        ball.simulated = (true);
        OptionEnimyPaddle.ConditionPause(false);
        OptionPlayerPaddle.ConditionPause(false);
        // Define as posições iniciais das raquetes
        playerPaddle.position = new Vector3(8f, 0f, 0f);
        enemyPaddle.position = new Vector3(-8f, 0f, 0f);
        ballController.ResetBall();
        OptionEnimyPaddle.ResetController();
        OptionPlayerPaddle.ResetController();
        playerScore = 0;
        enemyScore = 0;
        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();
        screenEndGame.SetActive(false);
        // ...
    }

    public void Exitgame()
    {
        Invoke("LoadMenu", 0.1f);
    }

    public void ScorePlayer()
    {
        OptionEnimyPaddle.ResetController();
        OptionPlayerPaddle.ResetController();
        playerScore++;
        textPointsPlayer.text = playerScore.ToString();
        CheckWin();
    }
    public void ScoreEnemy()
    {
        OptionEnimyPaddle.ResetController();
        OptionPlayerPaddle.ResetController();
        enemyScore++;
        textPointsEnemy.text = enemyScore.ToString();
        CheckWin();
    }

    
    public void CheckWin()
    {
        if (enemyScore >= winPoints || playerScore >= winPoints)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        screenEndGame.SetActive(true);
        string winner = SaveController.Instance.GetName(enemyScore > playerScore);
        textEndGame.text = "Vitória " + winner;

        string statistc = SaveController.Instance.nameEnemy + " = " + playerScore.ToString() + " X " + enemyScore.ToString() + " = " + SaveController.Instance.namePlayer;  

        SaveController.Instance.SaveWinner(winner, statistc);
        ball.simulated = (false);
        OptionEnimyPaddle.ConditionPause(true);
        OptionPlayerPaddle.ConditionPause(true);
        Invoke("LoadMenu", 5f);
    }


    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
