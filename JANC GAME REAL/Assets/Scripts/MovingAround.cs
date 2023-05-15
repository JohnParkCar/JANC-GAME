using UnityEngine;

public class MovingAround : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float dashSpeed = 20f;
    public float slideSpeed = 10f;
    public float jumpForce = 5f;
    public float wallJumpForce = 10f;
    public float wallJumpCooldown = 0.5f;

    private Rigidbody2D rb;
    private bool isSprinting = false;
    private bool isSliding = false;
    private bool isDashing = false;
    private bool isJumping = false;
    private bool canWallJump = true;
    private bool isTouchingWall = false;
    private float wallJumpCooldownTimer = 0f;

    private float moveX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LayerMask ledgeMask = LayerMask.GetMask ("Wall");

        // check on ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 2.8f, ledgeMask);

        Debug.DrawRay (transform.position, Vector3.down * 2.8f);

        isJumping = true;

        if (hit.collider != null) {
            isJumping = false;
        }

        // Walk
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * (isSprinting ? sprintSpeed : walkSpeed), rb.velocity.y);

        // Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            rb.velocity = new Vector2(moveX * dashSpeed, rb.velocity.y);
            Invoke(nameof(StopDash), 0.2f); // Stop dashing after 0.2 seconds
        }

        // Slide
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            isSliding = true;
            rb.velocity = new Vector2(moveX * slideSpeed, rb.velocity.y);
            Invoke(nameof(StopSlide), 0.5f); // Stop sliding after 0.5 seconds
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        // Wall Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && canWallJump && isTouchingWall)
        {
            isJumping = true;
            rb.velocity = new Vector2(moveX * jumpForce, wallJumpForce);
            canWallJump = false;
            wallJumpCooldownTimer = wallJumpCooldown;
        }

        // Wall Jump cooldown
        if (!canWallJump)
        {
            wallJumpCooldownTimer -= Time.deltaTime;
            if (wallJumpCooldownTimer <= 0)
            {
                canWallJump = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect if touching a wall
        if (collision.gameObject.CompareTag("Walls"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        // Detect if no longer touching a wall
        if (collision.gameObject.CompareTag("Walls"))
        {
            isTouchingWall = false;
        }
    }

    void StopDash()
    {
        isDashing = false;
    }

    void StopSlide()
    {
        isSliding = false;
    }
}