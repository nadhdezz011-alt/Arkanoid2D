using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bonusValue = 200;   // puntos extra al romperlo
    public float lifeTime = 5f;    // tiempo que permanece activo

    private void OnEnable()
    {
        // Desaparece automáticamente tras X segundos si no lo golpeas
        Invoke("Deactivate", lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(bonusValue);
            GameManager.Instance.BrickDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void Deactivate()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
