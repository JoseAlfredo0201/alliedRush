using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 10f; // The speed of the player
    private Rigidbody2D rb; // Reference to the Rigidbody component
    private Vector2 moveInput; // Store the movement input

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player

    }

    void Update()
    {
        // Get the horizontal and vertical input
        moveInput.x = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        moveInput.y = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
    }

    void FixedUpdate()
    {
        // Move the player based on input
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y); // Set the horizontal velocity based on input and keep the vertical velocity unchanged
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * speed); // Set the vertical velocity based on input
    }
}