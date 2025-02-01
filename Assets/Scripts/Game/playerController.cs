using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Movement
    // Player movement speed
    public float movementSpeed;


    // Jump 
    // Player jump height of first jump
    public float jumpHeight1;
    // Player jump height of second jump
    public float jumpHeight2;

    // Default: Set player to jump twice
    private int jumpCount = 2;

    // Dash
    public float dashSpeed;
    public float dashTime;
    float dashTimer;
    bool isDashing;
    bool canDash;

    // Player
    private Vector3 playerScale;
    private Rigidbody2D playerRigidBody;
    private CapsuleCollider2D playerFeet;
    private Animator playerAnimator;

    public GameWindow gameWindow;

    public void initPlayer() 
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerFeet = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerMove();
        playerDash();
        playerJump();
        playerFall();
        isLanded();
    }

    // Control player movement (A: Left, D: Right)
    private void playerMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = playerScale;
            playerRigidBody.velocity = new Vector2(movementSpeed, playerRigidBody.velocity.y);
            playerAnimator.SetBool("isRun", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            playerRigidBody.velocity = new Vector2(-movementSpeed, playerRigidBody.velocity.y);
            playerAnimator.SetBool("isRun", true);
        }
        else 
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            playerAnimator.SetBool("isRun", false);
        }
    }

    // Jump Function
    private void playerJump()
    {
        // Jump: If press "Space"
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            if (jumpCount == 2)
            {
                // Perform the first jump
                playerRigidBody.velocity = Vector2.up * jumpHeight1;
            }
            else if (jumpCount == 1)
            {
                // Perform the second jump
                playerRigidBody.velocity = Vector2.up * jumpHeight2;
            }

            // Decrement the jump count after any jump
            jumpCount--;
            playerAnimator.SetBool("isJump", true);
        }
        // Short jump: If short press "Space"
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (playerRigidBody.velocity.y > 3f) 
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 3f);
                playerAnimator.SetBool("isJump", true);

            }
        }
    }

    // Fall Function
    private void playerFall() 
    {
        // To limit the falling speed
        if (playerRigidBody.velocity.y < -8f) 
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -8f);
        }
    }

    // Dash Function
    private void playerDash()
    {
        if (isDashing)
        {
            // Reduce dash timer
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                // Stop dashing when the timer ends
                isDashing = false;
                playerRigidBody.gravityScale = 1.5f; // Reset gravity to normal
                playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y); // Stop horizontal movement
                playerAnimator.SetBool("isDashing", false);

            }
            else
            {
                // Maintain dash velocity without falling
                float dashDirection = transform.localScale.x > 0 ? -1 : 1; // Determine facing direction
                playerRigidBody.gravityScale = 0; // Disable gravity
                playerRigidBody.velocity = new Vector2(dashDirection * dashSpeed, 0); // Dash horizontally without vertical movement
            }
        }
        else
        {
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
            {
                // Start dashing
                isDashing = true;
                dashTimer = dashTime;
                canDash = false; // Disable further dashes until grounded
                playerAnimator.SetBool("isDashing", true);
            }
        }
    }

    // To check the player is landed
    private void isLanded() 
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCount = 2;
            canDash = true;
            playerAnimator.SetBool("isJump", false);

        }
        else 
        {
            if (jumpCount == 2) 
            {
                jumpCount--;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Spike")) 
        {
            gameWindow.gameOver();
        }
        else if (collision.transform.CompareTag("NextLevelPoint")) 
        {
            gameWindow.nextLevel();
        }
    }
}
