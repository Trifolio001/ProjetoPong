using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaddleController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3f;
    private GameObject ball;
    private bool option2Player = false;
    private bool activePause = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball"); // Encontra o objeto da bola na cena
    }

    void Update()
    {
        if (!activePause)
        {
            if (!option2Player)
            {
                if (ball != null)
                {
                    float targetY = Mathf.Clamp(ball.transform.position.y, -4.5f, 4.5f); // Limita a posição Y
                    Vector2 targetPosition = new Vector2(transform.position.x, targetY);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime
                   * speed); // Move gradualmente para a posição Y da bola
                }
            }
            else
            {
                // Captura da entrada vertical (teclas W e S)
                if (Input.GetKey(KeyCode.W))
                {
                    MovePlayer(1);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    MovePlayer(-1);
                }
            }
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

    public void ConditionPlayer(bool isPlayer)
    {
        option2Player = isPlayer;
    }

}
