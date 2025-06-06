using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastNonZeroInput = Vector2.right; // default facing right
    private Animator animator;
    private AudioSource stepAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stepAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;  // fixed property name too
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        bool isMoving = moveInput != Vector2.zero;

        if (isMoving)
        {
            lastNonZeroInput = moveInput;  // save last direction only if moving
        }

        animator.SetBool("isWalking", isMoving);
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastNonZeroInput.x);
            animator.SetFloat("LastInputY", lastNonZeroInput.y);

            if (stepAudio.isPlaying)
                stepAudio.Stop();
        }
        else if (isMoving && !stepAudio.isPlaying)
        {
            stepAudio.Play();
        }
    }
}

