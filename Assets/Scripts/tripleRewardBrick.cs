using UnityEngine;

public class tripleRewardBrick : MonoBehaviour
{
    public int rewardValue = 100;
    private int hitsRemaining = 3;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
                GameManager.Instance.AddScore(rewardValue * 3);
                GameManager.Instance.BrickDestroyed(this);

                GameManager.Instance.PlayBrickDestroySound();

                // Animación de destrucción con LeanTween
                LeanTween.scale(gameObject, Vector3.zero, 0.3f)
                         .setEaseInBack()
                         .setOnComplete(() => Destroy(gameObject));
            }
            else
            {
                if (hitsRemaining == 2) sr.color = Color.yellow;
                else if (hitsRemaining == 1) sr.color = Color.red;
            }
        }
    }
}
