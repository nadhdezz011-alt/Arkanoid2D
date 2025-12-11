using UnityEngine;

public class DoubleRewardBrick : MonoBehaviour
{
    public int rewardValue = 200;
    private int hitsRemaining = 2;

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
            hitsRemaining--;

            if (hitsRemaining <= 0)
            {
                GameManager.Instance.AddScore(rewardValue * 2);
                GameManager.Instance.BrickDestroyed(this);

                GameManager.Instance.PlayBrickDestroySound();

                // Animación de destrucción con LeanTween
                LeanTween.scale(gameObject, Vector3.zero, 0.3f)
                         .setEaseInBack()
                         .setOnComplete(() => Destroy(gameObject));
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
