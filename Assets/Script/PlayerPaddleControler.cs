using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleControler : MonoBehaviour
{
    public float speed = 10f;
    public string movementAxisNameAll = "Vertical";
    public string movementAxisNamePlayer = "Vertical";
    private bool activePause = false;
    public Rigidbody2D ball;
    public GameObject Player;
    public GameObject Bot;
    public GameObject Playerrotate;
    public GameObject Botrotate;
    public float SetingAngle;

    enum STATEPLAYER { PLAYER1, PLAYER2, COMPUTER}
    STATEPLAYER stateplayer;

    //test IA
    enum STATES { WAITING, REASONING, ACTING }

    List<Vector3> trajectoryPoints = new List<Vector3>();

    STATES state;

    private float distanceToReact = 1f;
    private float uncertaintyPosition = 0.8f;

    private float randomMove = 2;
    private float randomMoveInterval = 1.0f;
    public int side = 1;

    private float topLimit = 4.8f;
    private float botLimit = -4.8f;

    private float currentPosition;
    private float targetPosition;

    private bool showTrajectory = false;

    public bool isPlayer = true;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;


    private void Start()
    {

        if (isPlayer)
        {
            spriteRenderer1.color = SaveController.Instance.colorPlayer;
            spriteRenderer2.color = SaveController.Instance.colorPlayer;
            if (SaveController.Instance.IdentyPlayer == 0)
            {
                if ((SaveController.Instance.IdentyPlayer == 0) && (SaveController.Instance.IdentyEnemy == 0))
                {
                    ConditionPlayer(0);
                }
                else
                {
                    ConditionPlayer(1);
                }
            }
            else
            {
                ConditionPlayer(SaveController.Instance.IdentyPlayer+1);
            }
        }
        else
        {
            spriteRenderer1.color = SaveController.Instance.colorEnemy;
            spriteRenderer2.color = SaveController.Instance.colorEnemy;
            if (SaveController.Instance.IdentyEnemy == 0)
            {
                if ((SaveController.Instance.IdentyPlayer == 0) && (SaveController.Instance.IdentyEnemy == 0))
                {
                    ConditionPlayer(0);
                }
                else
                {
                    ConditionPlayer(1);
                }
            }
            else
            {
                ConditionPlayer(SaveController.Instance.IdentyEnemy+1);
            }
        }
    }

    void Update()
    {
        if (!activePause)
        {
            switch (stateplayer)
            {
                case STATEPLAYER.COMPUTER:
                    if (ball != null)
                    {
                        IABot();
                        animationBot();
                    }
                    break;
                case STATEPLAYER.PLAYER1:
                    float moveInput0 = Input.GetAxis(movementAxisNamePlayer);
                    MovePlayer(moveInput0);
                    animationPLayer();
                    break;
                case STATEPLAYER.PLAYER2:
                    float moveInput1 = Input.GetAxis(movementAxisNameAll);
                    MovePlayer(moveInput1);
                    animationPLayer();
                    break;
            }
        }
    }

    public void ResetController()
    {
        state = STATES.WAITING;
        animationPLayer();
        if (stateplayer == STATEPLAYER.COMPUTER)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }            
        animationBot();
    }

    public void animationPLayer()
    {
        
        Vector3 targetPosition = ball.transform.position;
        Vector3 playerPosition = Player.transform.position;
        //cria uma animação seguindo o angulo da bola, o efeito teve que ser diminuido para o player
        //pois a animação diminui a precisão da colizão muitas vezes atrapalhando o controle do jogador
        if (isPlayer) {
            playerPosition = new Vector2(Player.transform.position.x + 5, Player.transform.position.y);
        }
        else
        {
            playerPosition = new Vector2(Player.transform.position.x - 5, Player.transform.position.y);
        }
        Playerrotate.transform.localRotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2((targetPosition.y - playerPosition.y), targetPosition.x - playerPosition.x) * Mathf.Rad2Deg));
   
    }

    public void animationBot()
    {
        Vector3 targetPosition = ball.transform.position;
        Vector3 playerPosition = Bot.transform.position;
        //cria uma animação seguindo o angulo da bola, onde é diminuido quando a bola estiver na mesma altura
        //pois a bola pega efeito conforme o angulo do escudo, assim diminuido a possibilidade da bola ficar
        //apenas batendo nas laterais
        float Dist = 1.3f;
        if ((targetPosition.y - playerPosition.y < Dist) && (targetPosition.y - playerPosition.y > -Dist))
        {
            Botrotate.transform.localRotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2((targetPosition.y - playerPosition.y) / 5, targetPosition.x - playerPosition.x) * Mathf.Rad2Deg));
        }
        else
        {
            Botrotate.transform.localRotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2(targetPosition.y - playerPosition.y, targetPosition.x - playerPosition.x) * Mathf.Rad2Deg));
        }
    }

    public void MovePlayer(float moveInput)
    {

        // Calcula a nova posição da raquete baseada na entrada e na velocidade
        Vector3 newPosition = transform.position + Vector3.up * moveInput * speed * Time.deltaTime;
        // Limita a posição vertical da raquete para que ela não saia da tela
        newPosition.y = Mathf.Clamp(newPosition.y, -4.5f, 4.5f);
        // Atualiza a posição da raquete
        transform.position = newPosition;
    }

    public void ConditionPause(bool isPause)
    {
        activePause = isPause;
    }

    public void ConditionPlayer(int StatusPlayer)
    {
        //Aqui determina se é um jogador, qual tipo de controle 
        //e também determina se sera um Bot e sua velocidade (Dificuldade)
        switch (StatusPlayer) {
            case 0:
                stateplayer = STATEPLAYER.PLAYER1;
                Bot.SetActive(false);
                Player.SetActive(true);
                speed = 9f;
                break;
            case 1:
                stateplayer = STATEPLAYER.PLAYER2;
                Bot.SetActive(false);
                Player.SetActive(true);
                speed = 9f;
                break;
            case 2:
                stateplayer = STATEPLAYER.COMPUTER;
                Bot.SetActive(true);
                Player.SetActive(false);
                speed = 6f;
                break;
            case 3:
                stateplayer = STATEPLAYER.COMPUTER;
                Bot.SetActive(true);
                Player.SetActive(false);
                speed = 8f;
                break;
            case 4:
                stateplayer = STATEPLAYER.COMPUTER;
                Bot.SetActive(true);
                Player.SetActive(false);
                speed = 10f;
                break;
            case 5:
                stateplayer = STATEPLAYER.COMPUTER;
                Bot.SetActive(true);
                Player.SetActive(false);
                speed = 13f;
                break;

        }   
    }

    //Ideia adaptada do canal (Programando Games)
    //IA basicamente cria três estados
    //o Status de +ESPERA+ iniciado quando a bola se afasta ou não esta proxima de uma determinda "distancia de reação"
    //fazendo movimento seguindo a bola em tempos aleatorios
    //o Status de +RACIOCINIO+ é chamado quando a bola se aprocima e esta dentro da zona "distancia de reação"
    //simulando a trajetoria da bola , tambem faz um calculo randomico para a bola não rebater apenas no meio do personagem
    //o Status de +AÇÂO+ é disparado apos o raciocinio posicionando o personagem na posição calculada anteriormente

    public void IABot()
    {
        switch (state)
        {

            case STATES.WAITING:
                if (side == 1)
                {
                    if (Mathf.Sign(ball.velocity.x) > 0 && ball.transform.position.x >= distanceToReact){
                        state = STATES.REASONING;
                    }
                }
                else
                {
                    if (Mathf.Sign(ball.velocity.x) < 0 && ball.transform.position.x <= -distanceToReact)
                    {
                        state = STATES.REASONING;
                    }
                }

                if (side == 1)
                {
                    if (Time.time >= randomMove && ball.transform.position.x <= distanceToReact)
                    {
                        targetPosition = ball.transform.position.y;
                        randomMove = Time.time + randomMoveInterval;
                    }
                }
                else
                {
                    if (Time.time >= randomMove && ball.transform.position.x >= -distanceToReact)
                    {
                        targetPosition = ball.transform.position.y;
                        randomMove = Time.time + randomMoveInterval;
                    }
                }

                // Aplica o movimento
                GoToPosition();
                break;

            case STATES.REASONING:

                // calcula a trajetória
                SimulateBallTrajectory();

                currentPosition = transform.position.y;

                float targetUp = targetPosition + uncertaintyPosition;
                float targetDown = targetPosition - uncertaintyPosition;
                targetPosition = Random.Range(targetDown, targetUp);

                // após tudo terminar, muda o estado pra AGIR
                state = STATES.ACTING;
                break;

            case STATES.ACTING:
                GoToPosition();

                // OBS: o Paddle esta na posição 8f e a chamada do estado de espera no 7f, assim, 
                // chamando antes de rebater, muitas vezes causando um movimento aleatorio antes da bola chegar
                // criando mais uma incerteza do efeito da direção da bola

                if (side == 1)
                {
                    if (Mathf.Sign(ball.velocity.x) < 0 || ball.transform.position.x >= 7f)
                    {
                        state = STATES.WAITING;
                    }
                }
                else
                {
                    if (Mathf.Sign(ball.velocity.x) > 0 || ball.transform.position.x <= -7f)
                    {
                        state = STATES.WAITING;
                    }
                }
                break;
        }

    }

    // Faz o Paddle se mover suavemente para a posição alvo, respeitando os limites da tela
    void GoToPosition()
    {

        currentPosition = Mathf.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, currentPosition);

        transform.position = new Vector3(transform.position.x, 
            Mathf.Clamp(transform.position.y, botLimit + transform.localScale.y / 2, topLimit - transform.localScale.y / 2));

    }

    void SimulateBallTrajectory()
    {

        int iterations = 500;
        float step = 0.01f;

        Vector2 position = ball.transform.position;
        Vector2 velocity = ball.velocity;

        while (iterations > 0)
        {
            position += velocity * step;

            if (position.y >= topLimit || position.y <= botLimit)
            {
                velocity.y *= -1;
            }

            if (side == 1)
            {
                if (position.x >= 8.2f)
                {
                    targetPosition = position.y;
                    break;
                }
            }
            else
            {
                if (position.x <= -8.2f)
                {
                    targetPosition = position.y;
                    break;
                }
            }

            --iterations;

            if (showTrajectory)
            {
                trajectoryPoints.Add(new Vector3(position.x, position.y, 1f));
            }

        }
    }



}