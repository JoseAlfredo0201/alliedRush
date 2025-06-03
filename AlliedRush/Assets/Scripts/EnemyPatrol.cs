using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;            // Movement speed
    public float patrolDistance = 3f;   // Distance to move before turning

    private Vector2 startPosition;
    private int direction = 1;          // 1 = right, -1 = left
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Move the enemy left or right
        rb.linearVelocity = new Vector2(direction * speed, 0f);

        // Check how far it has moved from the starting position
        float distanceMoved = transform.position.x - startPosition.x;

        // If moved beyond patrolDistance in either direction, reverse
        if (Mathf.Abs(distanceMoved) >= patrolDistance)
        {
            direction *= -1; // Reverse direction
            startPosition = transform.position; // Reset starting point
        }
    }
}

