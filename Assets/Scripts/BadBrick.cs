using UnityEngine;

public class BadBrick : MonoBehaviour
{
    public int penaltyValue = -50;
    public float lifeTime = 5f;

    [Header("Movimiento horizontal")]
    public float moveSpeed = 2f;       // velocidad de movimiento
    public float moveRange = 0.5f;       // rango de oscilación
    private Vector3 startPos;
    private float direction = 1f;

    private void OnEnable()
    {
        startPos = transform.position;
        direction = 1f;
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void Update()
    {
        // Movimiento horizontal oscilante
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Si se pasa del rango, invertimos dirección
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveRange)
        {
            direction *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(penaltyValue);

            // Notificamos al GameManager para liberar la posición con delay
            GameManager.Instance.BrickDestroyed(this);

            gameObject.SetActive(false);
        }
    }

    private void Deactivate()
    {
        if (gameObject.activeSelf)
        {
            GameManager.Instance.BrickDestroyed(this); // libera con delay
            gameObject.SetActive(false);
        }
    }
}

