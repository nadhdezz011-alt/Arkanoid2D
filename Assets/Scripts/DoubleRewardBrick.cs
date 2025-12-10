using UnityEngine;

public class DoubleRewardBrick : MonoBehaviour
{
    public int rewardValue = 200;   // valor base de recompensa
    private int hitsRemaining = 2;  // necesita dos golpes

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitsRemaining--;

            if (hitsRemaining <= 0)
            {
                // Al romperse da el doble de recompensa
                GameManager.Instance.AddScore(rewardValue * 2);
                GameManager.Instance.BrickDestroyed(this);
                Destroy(gameObject);
            }
            else
            {
                // Feedback opcional: cambiar color, animación, etc.
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
