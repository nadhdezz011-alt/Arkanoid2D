using UnityEngine;

public class PowerUpBrick : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float lifeTime = 5f;

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (powerUpPrefab != null)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            GameManager.Instance.BrickDestroyed(this);

            gameObject.SetActive(false);
        }
    }

    private void Deactivate()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
