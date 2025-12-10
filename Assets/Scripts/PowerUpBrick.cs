using UnityEngine;

public class PowerUpBrick : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnDelay = 2f; // segundos de retraso para el corazón

    private bool pendingSpawn = false;
    private float timer = 0f;
    private Vector3 spawnPosition;

    private void Update()
    {
        if (pendingSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnDelay)
            {
                if (powerUpPrefab != null)
                {
                    Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
                }

                // Notificamos al GameManager para liberar la posición con delay
                GameManager.Instance.BrickDestroyed(this);

                gameObject.SetActive(false);
                pendingSpawn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Guardamos la posición antes de desactivar
            spawnPosition = transform.position;

            // Activamos temporizador para instanciar el corazón
            pendingSpawn = true;
            timer = 0f;
        }
    }
}
