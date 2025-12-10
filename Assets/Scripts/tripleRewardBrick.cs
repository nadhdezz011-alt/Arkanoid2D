using UnityEngine;

public class tripleRewardBrick : MonoBehaviour
{
    public int rewardValue = 100;   // valor base de recompensa
    private int hitsRemaining = 3;  // necesita tres golpes
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitsRemaining--;

            if (hitsRemaining <= 0)
            {
                // Al romperse da el triple de recompensa
                GameManager.Instance.AddScore(rewardValue * 3);
                GameManager.Instance.BrickDestroyed(this);
                Destroy(gameObject);
            }
            else
            {
                // Cambiar color según los golpes restantes
                if (hitsRemaining == 2)
                {
                    sr.color = Color.yellow; // primer golpe  amarillo
                }
                else if (hitsRemaining == 1)
                {
                    sr.color = Color.red; // segundo golpe rojo
                }
            }
        }
    }
}
