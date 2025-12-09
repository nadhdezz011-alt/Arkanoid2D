using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public Rigidbody2D rb2D;
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
        transform.position = startPosition;
        rb2D.Sleep();
        rb2D.WakeUp();

        Vector2 dir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb2D.AddForce(dir * 8f, ForceMode2D.Impulse);
    }
}
