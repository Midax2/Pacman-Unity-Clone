using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Player speed
    private float moveX, moveY;
    private Vector2 lastMoveDirection = new Vector2(1, 0);
    private bool facingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovement();
    }

    /// <summary>
    /// Handles the player's movement based on input.
    /// </summary>
    private void PlayerMovement()
    {
        // Get input from the player
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveX = moveX < 0 ? -1 : (moveX > 0 ? 1 : 0);
        moveY = moveY < 0 ? -1 : (moveY > 0 ? 1 : 0);

        Vector2 moveDirection = CalculateDirection();

        // Move the player in the direction of the input
        rb.linearVelocity = moveDirection * moveSpeed;

        // Rotate the player to face the direction of movement
        if (moveDirection != Vector2.zero)
        {
            // Flip the player sprite left to right and vice verse if the direction changes
            if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
            {
                FlipPlayerSprite();
                return;
            }
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            // Keep the current rotation if no input is provided
            transform.rotation = transform.rotation;
        }
    }

    /// <summary>
    /// Calculate the movement direction.
    /// </summary>
    private Vector2 CalculateDirection()
    {

        if (moveX != 0 && moveY != 0) return lastMoveDirection; // Prevent diagonal movement

        Vector2 moveDirection = new Vector2(moveX, moveY);

        // If there is no input, keep the last move direction
        if (moveDirection == Vector2.zero)
        {
            moveDirection = lastMoveDirection;
        }
        else
        {
            lastMoveDirection = moveDirection;
        }

        return moveDirection;
    }

    /// <summary>
    /// Flip the player sprite left to right and vice versa.
    /// </summary>
    private void FlipPlayerSprite()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.y *= -1;
        transform.localScale = playerScale;
    }

    /// <summary>
    /// Logs collision with other rigidbody objects.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
        {
            Debug.Log($"Player collided with {collision.gameObject.name}");
        }
    }
}
