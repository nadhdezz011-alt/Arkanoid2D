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
        
        velocity.x = Random.Range(-1f, 1f);
        velocity.y = 1f;
        rb2D.AddForce(velocity * speed);
    }
}
