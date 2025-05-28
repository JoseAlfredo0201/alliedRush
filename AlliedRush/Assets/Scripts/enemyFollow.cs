using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyFollow : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float detectionRange; // Range within which the enemy can detect the player
    public float speed; // Speed of the enemy
    private float speedy; // Speed in the y direction
    private float speedx; // Speed in the x direction
    private float distance; // Distance to the player
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 posicion; // Position of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedy = speed;
        posicion = transform.position;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player
        rb.linearVelocity = new Vector2(0, speed); // Set the initial velocity of the object
        rb.freezeRotation = true; 
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); // Calculate the distance to the player
        if (distance <= detectionRange) // If the player is within a certain distance
        {
            Vector2 direction = (player.transform.position - transform.position).normalized; // Calculate the direction to the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // Move towards the player
        }
        else
        {
            posicion = transform.position; // Get the current position of the object
            //check the objects position and set the velocity of the object
            if (posicion.y > 2) {
                speedy = -speed;
            } else if (posicion.y < -2){
                speedy = speed;
            }

            if (posicion.x == 18.86){
                speedx = 0;
            } else if (posicion.x < 18.86){
                speedx = speed;
            } else {
                speedx = -speed;
            }
            rb.linearVelocity = new Vector2(speedx, speedy); // Set the velocity of the object
        }
    }
}
