using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f; // Movement speed of the player
    private Rigidbody2D rb;   // Reference to the Rigidbody2D component
    private Vector2 moveInput; // Stores the current movement input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevent the player from rotating when colliding
    }

    void Update()
    {
        // Capture player input
        moveInput.x = Input.GetAxis("Horizontal"); // A/D or Left/Right
        moveInput.y = Input.GetAxis("Vertical");   // W/S or Up/Down
    }

    void FixedUpdate()
    {
        // Apply movement based on input
        rb.linearVelocity = moveInput * speed;
    }
}

