using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Rigidbody2D rb;
    private float inputValue;
    public float moveSpeed = 25f;
    private Vector2 moveDirection;
    private Vector2 startPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        inputValue = Input.GetAxis("Horizontal");
        if (inputValue == 1)
        {
            moveDirection = Vector2.right;
        }
        else if (inputValue == -1)
        {
            moveDirection = Vector2.left;
        }
        else
        {
            moveDirection = Vector2.zero;
        }
        rb.AddForce(moveDirection * moveSpeed * Time.deltaTime * 100);
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        
    }
}
