using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bonusValue = 200;
    public float fallSpeed = 0.5f;
    public float lifeTime = 5f;   // tiempo máximo en pantalla

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(bonusValue);
            GameManager.Instance.GainHeart();

            //  Quitamos BrickDestroyed(this)
            gameObject.SetActive(false);
        }
    }

}
