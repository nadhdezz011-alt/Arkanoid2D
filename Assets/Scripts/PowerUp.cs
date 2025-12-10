using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bonusValue = 200;
    public float fallSpeed = 1f;
    public float lifeTime = 10f;   // tiempo máximo en pantalla

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Contador de tiempo
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(bonusValue);
            GameManager.Instance.GainHeart();
            GameManager.Instance.BrickDestroyed(this);

            gameObject.SetActive(false);
        }
    }
}
