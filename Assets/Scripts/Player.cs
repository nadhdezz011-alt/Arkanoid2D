using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private float inputValue;
    public float moveSpeed = 25f;
    private Vector2 moveDirection;
    

    void Update()
    {
        ///pulsa "a" o flecha izquierda = -1//pulsa "d" o flecha derecha = 1//no pulsa nada = 0
        inputValue = Input.GetAxis("Horizontal");
        if (inputValue == 1)
        {             moveDirection = Vector2.right;
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
}
