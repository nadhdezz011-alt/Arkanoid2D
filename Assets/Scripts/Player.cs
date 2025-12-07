using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Rigidbody2D rb;
    public float moveSpeed = 25f;
    private float inputValue;
    private Vector2 moveDirection;
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
        inputValue = Input.GetAxis("Horizontal");
        if (inputValue == 1)
            moveDirection = Vector2.right;
        else if (inputValue == -1)
            moveDirection = Vector2.left;
        else
            moveDirection = Vector2.zero;

        rb.AddForce(moveDirection * moveSpeed * Time.deltaTime * 100);
    }

    public void ResetPlayer()
    {
        // Reposicionar al jugador
        transform.position = startPosition;

        // Dormir y despertar el rigidbody para limpiar fuerzas
        rb.Sleep();
        rb.WakeUp();
    }
}
