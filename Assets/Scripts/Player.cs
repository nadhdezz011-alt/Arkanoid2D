using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Rigidbody2D rb;
    public float moveSpeed = 25f;
    private Vector2 startPosition;

    private void Awake()
    {
        Instance = this;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float inputValue = Input.GetAxis("Horizontal");

        // Movimiento con AddForce (fluido)
        rb.AddForce(Vector2.right * inputValue * moveSpeed);

        // Limitar a los bordes
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8f, 8f);
        transform.position = pos;
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        rb.Sleep();
        rb.WakeUp();
    }
}
