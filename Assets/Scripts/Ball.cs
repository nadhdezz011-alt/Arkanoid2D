using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public Rigidbody2D rb2D;
    private Vector2 startPosition;
    private bool isAttached = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startPosition = transform.position;
        ResetBall();
    }

    void Update()
    {
        if (isAttached)
        {
            // La bola sigue al paddle
            transform.position = Player.Instance.transform.position + Vector3.up * 0.5f;

            // Lanzar con Jump
            if (Input.GetButtonDown("Jump"))
            {
                LaunchBall();
            }
        }
    }

    public void LaunchBall()
    {
        isAttached = false;
        rb2D.Sleep();
        rb2D.WakeUp();

        // Fuerza inicial hacia arriba
        rb2D.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
    }

    public void ResetBall()
    {
        isAttached = true;
        transform.position = startPosition;
        rb2D.Sleep();
        rb2D.WakeUp();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Diferencia horizontal entre bola y paddle
            float xOffset = transform.position.x - collision.transform.position.x;

            // Nueva dirección con inclinación
            Vector2 newDir = new Vector2(xOffset, 1f).normalized;

            // Reiniciamos y aplicamos impulso fijo
            rb2D.Sleep();
            rb2D.WakeUp();
            rb2D.AddForce(newDir * 8f, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.Instance.LoseHeart();
        }
    }
}
