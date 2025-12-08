using UnityEngine;

public class Brick : MonoBehaviour
{
    private void Start()
    {
        // Avisamos al GameManager que este ladrillo existe
        GameManager.Instance.RegisterBrick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Avisamos al GameManager que este ladrillo se destruyó
            GameManager.Instance.BrickDestroyed();

            Destroy(gameObject);
        }
    }
}
