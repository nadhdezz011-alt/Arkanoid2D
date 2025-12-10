using UnityEngine;

public class PowerUpBrick : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnDelay = 2f; // segundos de retraso

    private bool pendingSpawn = false;
    private float timer = 0f;

    private void Update()
    {
        if (pendingSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnDelay)
            {
                if (powerUpPrefab != null)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                // Ahora sí desactivamos el bloque
                gameObject.SetActive(false);

                pendingSpawn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.BrickDestroyed(this);

            // activa el temporizador para instanciar después
            pendingSpawn = true;
            timer = 0f;
        }
    }
}
