using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Params")]
    Vector2 moveInput;
    [SerializeField] float velocityAmount = 5f;
    [SerializeField] float jumpAmount = 5f;
    [SerializeField] float climbAmount = 2f;
    float gravityScaleStart;

    [Header("Initiators")]
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;

    [Header("Death")]
    bool isAlive;
    [SerializeField] Vector2 deathkick = new Vector2(10f, 10f);

    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    GameSession gameSession;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleStart = rb.gravityScale;

        isAlive = true;
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSpriteMove();
        ClimbLadder();
        checkDeath();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if ( !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !isAlive ) { return; }

        if (value.isPressed)
        {
            rb.velocity += new Vector2 (0f, jumpAmount);
        }
    }

    void OnFire()
    {
        if (!isAlive) { return; }
        Instantiate(arrow, bow.position, transform.rotation);
        animator.SetTrigger("isShooting");
    }
    void Run()
    {
        Vector2 runVelocity = new Vector2(moveInput.x * velocityAmount, rb.velocity.y);
        rb.velocity = runVelocity;
    }

    
    void FlipSpriteMove() {
        bool playerIsMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerIsMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            animator.SetBool("isRunning", true);
        }
        animator.SetBool("isRunning", playerIsMovingHorizontally);
    }
    void FlipSpriteClimb()
    {
        bool playerIsClimbing = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        
        animator.SetBool("isClimbing", playerIsClimbing);
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            rb.gravityScale = gravityScaleStart;
            animator.SetBool("isClimbing", false);
            return; 
        }

        rb.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbAmount);
        rb.velocity = climbVelocity;
        FlipSpriteClimb();

    }

    void checkDeath()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("isDying");
            rb.velocity = deathkick;
            gameSession.PlayerDeath();
        }
    }

}
