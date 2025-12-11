using UnityEngine;

public class PowerUpBrick : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnDelay = 2f;

    private bool pendingSpawn = false;
    private float timer = 0f;
    private Vector3 spawnPosition;

    private void Start()
    {
        GameManager.Instance.BrickSpawned();

        // Animación inicial
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseOutBack();
    }

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

                GameManager.Instance.BrickDestroyed(this);

                GameManager.Instance.PlayBrickDestroySound();

                // Animación de destrucción con LeanTween
                LeanTween.scale(gameObject, Vector3.zero, 0.3f)
                         .setEaseInBack()
                         .setOnComplete(() => Destroy(gameObject));

                pendingSpawn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            spawnPosition = transform.position;
            pendingSpawn = true;
            timer = 0f;
        }
    }
}
