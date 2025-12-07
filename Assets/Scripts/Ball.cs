using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed = 300f;
    Vector2 Velocity;

    void Start()
    {
        Velocity.x = Random.Range(-1f, 1f);
        Velocity.y = 1f;
        rb2D.AddForce(Velocity * speed);
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Dead"))
       {
           FindObjectOfType<GameManager>().LoseHeart();
        }
    }
}
