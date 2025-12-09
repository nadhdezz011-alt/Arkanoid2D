using UnityEngine;

public class BadBrick : MonoBehaviour
{
    public int penaltyValue = -50;
    public float lifeTime = 5f;

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(penaltyValue);
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
