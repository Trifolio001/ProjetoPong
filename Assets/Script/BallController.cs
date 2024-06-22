using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float startingVelocity = 8f;
    private Rigidbody2D rb;
    public GameManager gameManager;
    public float SpeedUp = 1.1f;
    public GameObject RefBola;

    public Vector2 velocity;



    public void ResetBall()
    {
        RefBola.SetActive(false);
        transform.position = Vector3.zero;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        // gera um numero randomico para o angulo que a bola sera lançada
        // Obs o numero negativo gera perante o angulo y e o positivo angulo x 
        float RandomStart = (Random.Range(0f, 45f));

        // Gera um número aleatório entre positivo e negativo para diversificar a posição em que a bola sera lançada
        float x = ((int)Random.Range(0, 2) * 2 - 1) * (startingVelocity) * Mathf.Cos(RandomStart * Mathf.Deg2Rad);
        float y = ((int)Random.Range(0, 2) * 2 - 1) * (startingVelocity) * Mathf.Sin(RandomStart * Mathf.Deg2Rad);

        //esta condição de tempo serve tanto para dar tempo do jogador se posicionar para primeira rebatida
        //quanto para iniciar o posicionamento do bot, isso evita do primeiro lance ser um ponto direto sempre 
        StartCoroutine(RandomDirectionBall(x, y, 0.5f));
    }

    IEnumerator RandomDirectionBall(float x, float y, float time)
    {
        yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(x/3, y/3);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(x / 2, y / 2);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(x, y);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }

        
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {

            //este calculo pega a direção que a bola esta traçando ao rebater com o objeto e mistura com
            //o a posição em que a bola bateu no objeto (traçando um angulo referenciando o centro do objeto
            //ao lado oposto que a bola colidiu) pegando a metade da influencia de cada uma dessas duas influencias
            //montando a nova direção
            float direction = Mathf.Sign(-rb.velocity.x);
            float Angle1 = Mathf.Rad2Deg * (Mathf.Atan2(rb.velocity.y, Mathf.Abs(rb.velocity.x)));
            float Angle2 = Mathf.Rad2Deg * (Mathf.Atan2(gameObject.transform.position.y - collision.transform.position.y, Mathf.Abs(gameObject.transform.position.x - collision.transform.position.x)));
            float NewAngle = (Angle1 + Angle2) / 2;                        
            float OldVelocity = rb.velocity.magnitude;
            float x = OldVelocity * Mathf.Cos(NewAngle * Mathf.Deg2Rad);
            float y = OldVelocity * Mathf.Sin(NewAngle * Mathf.Deg2Rad);
            rb.velocity = new Vector2(direction * x, y);

            comportamentBall();
            rb.velocity *= SpeedUp;
        }
        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            gameManager.ScorePlayer();
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("WallPlayer"))
        {
            gameManager.ScoreEnemy();
            ResetBall();
        }

        //com a animação a bola começou a ter a possibilidade de ser empurrada para fora das paredes  
        //então foi incluido um reset caso aconteça
        if (collision.gameObject.CompareTag("Lost"))
        {
            RefBola.SetActive(true);
            transform.position = Vector3.zero;
            rb.velocity = Vector2.zero;
            StartCoroutine(ResetBolaFora(3f));
        }
    }

    IEnumerator ResetBolaFora(float time)
    {
        yield return new WaitForSeconds(time);
        ResetBall();
    }


    public void comportamentBall()
    {
        float numx = rb.velocity.x;

        float numy = rb.velocity.y;
        if (numx < 0)
        {
            numx *= -1;
        }
        if (numy < 0)
        {
            numy *= -1;
        }
        float velocity = numx + numy;
    }

}
