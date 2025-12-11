using UnityEngine;

public class RewardBrick : MonoBehaviour
{
    public int rewardValue = 100;

    private void Start()
    {
        GameManager.Instance.BrickSpawned();

        // Animación inicial
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseOutBack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(rewardValue);
            GameManager.Instance.BrickDestroyed(this);

            //  Sonido de destrucción
            GameManager.Instance.PlayBrickDestroySound();

            // Animación de destrucción con LeanTween
            LeanTween.scale(gameObject, Vector3.zero, 0.3f)
                     .setEaseInBack()
                     .setOnComplete(() => Destroy(gameObject));
        }
    }
}

