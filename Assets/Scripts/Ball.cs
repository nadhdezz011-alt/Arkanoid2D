using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public Rigidbody2D rb2D;
    public float speed = 300f;
    private Vector2 velocity;
    private Vector2 startPosition;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startPosition = transform.position;
        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.Instance.LoseHeart();
        }
    }

    public void ResetBall()
    {
        // Reposicionar en el punto inicial
        transform.position = startPosition;

        // Dormir y despertar el rigidbody para limpiar fuerzas acumuladas
        rb2D.Sleep();
        rb2D.WakeUp();

        // Dirección aleatoria hacia arriba
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;

        // Impulso moderado
        rb2D.AddForce(dir * 8f, ForceMode2D.Impulse);
    }

}
