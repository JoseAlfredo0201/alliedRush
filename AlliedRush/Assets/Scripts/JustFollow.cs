using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustFollow : MonoBehaviour
{
    public GameObject player;
    public float detectionRange = 8f;
    public float speed = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= detectionRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop when outside detection range
        }
    }
}


