using UnityEngine;

public class RewardBrick : MonoBehaviour
{
    public int rewardValue = 100; // puntos positivos

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(rewardValue);
            GameManager.Instance.BrickDestroyed(this);
            Destroy(gameObject);
        }
    }
}
