using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // Movement
    // Player movement speed
    public float movementSpeed;
    public float input;

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

    // Main Menu Interactions
    private GameObject keyIconPlay;
    private GameObject keyIconExit;
    private bool isPlayerNearby;
    private string objectTag = ""; // Store the nearby object tag

    // KnockBack Effect
    public float knockBackForce;
    public float knockBackCounter;
    public float knockBackDuration;
    public bool knockBackFromRight;

    // Clamping position
    private Camera mainCam;
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

    // Reference
    public GameWindow gameWindow;

    public void initPlayer() 
    {
        mainCam = Camera.main;
        screenBounds = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerFeet = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        isPlayerNearby = false;

        // Find key icons by tag
        keyIconPlay = GameObject.Find("f_button_play");
        keyIconExit = GameObject.Find("f_button_exit");

        // Hide key icons at start
        if (keyIconPlay) keyIconPlay.SetActive(false);
        if (keyIconExit) keyIconExit.SetActive(false);
    }

    private void Update()
    {
        // To get the input direction
        input = Input.GetAxisRaw("Horizontal");

        playerMove();
        playerDash();
        playerJump();
        playerFall();
        isLanded();

        clampPosition();

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            keyInteraction();
        }
    }

    // Control player movement (A: Left, D: Right)
    private void playerMove()
    {
        if (knockBackCounter <= 0)
        {
            playerRigidBody.velocity = new Vector2(input * movementSpeed, playerRigidBody.velocity.y);
        }
        else 
        {
            if (knockBackFromRight == true) 
            {
                playerRigidBody.velocity = new Vector2(-knockBackForce, 0);
            }
            if (knockBackFromRight == false) 
            {
                playerRigidBody.velocity = new Vector2(knockBackForce, 0);
            }

            knockBackCounter -= Time.deltaTime;
        }

        if (input < 0)
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            playerAnimator.SetBool("isRun", true);
        }
        else if (input > 0)
        {
            transform.localScale = playerScale;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StartGameSign"))
        {
            isPlayerNearby = true;
            objectTag = "StartGameSign";
            if (keyIconPlay) keyIconPlay.SetActive(true);
        }
        else if (other.CompareTag("ExitGameSign"))
        {
            isPlayerNearby = true;
            objectTag = "ExitGameSign";
            if (keyIconExit) keyIconExit.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StartGameSign") || other.CompareTag("ExitGameSign"))
        {
            isPlayerNearby = false;
            objectTag = "";
            if (keyIconPlay) keyIconPlay.SetActive(false);
            if (keyIconExit) keyIconExit.SetActive(false);
        }
    }

    private void keyInteraction()
    {
        if (objectTag == "StartGameSign")
        {
            gameWindow.nextLevel();
        }
        else if (objectTag == "ExitGameSign")
        {
            Application.Quit();
        }
    }

    private void clampPosition()
    {
        // Clamp player position within screen bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);

        // Top boundary
        if (transform.position.y > screenBounds.y - playerHeight)
        {
            clampedPosition.y = screenBounds.y - playerHeight;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0); // Stop upward movement to prevent stick at the top
        }

        // Bottom boundary
        if (transform.position.y < -screenBounds.y + playerHeight)
        {
            gameWindow.gameOver();
            return;
        }

        transform.position = clampedPosition;
    }
}