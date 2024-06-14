using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerHealth playerHealth;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask trapLayer;

    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpAudioSource; 

    private int jumpCount = 0; // Counter for the number of jumps
    private const int MaxJumps = 1; // Maximum number of jumps

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>(); // Get the PlayerHealth component
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player.");
        }
        
    }

    private void Update()
    {
        // Check if the player is dead. If so, don't update movement.
        if (playerHealth != null && playerHealth.IsDead())
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MaxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            PlayJumpSound(); // Play jump sound
        }

        UpdateAnimationState();
    }


    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void PlayJumpSound()
    {
        if (jumpAudioSource != null && jumpAudioSource.clip != null)
        {
            // Debug.Log("jumping audio soruce played");
            jumpAudioSource.Play();
        }
    }

    // In PlayerMovement script
    public void ModifySpeed(float multiplier, float duration)
    {
        StartCoroutine(AdjustSpeed(multiplier, duration));
    }

    private IEnumerator AdjustSpeed(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;

        yield return new WaitForSeconds(5f); // Change to 5 seconds

        moveSpeed = originalSpeed;
    }


}

