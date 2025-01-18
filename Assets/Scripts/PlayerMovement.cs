using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnDetectionDistance = 1f;
    private float moveX, moveY;
    private Vector2 lastMoveDirection = new Vector2(1, 0);
    private Vector2 inputDirection = Vector2.zero;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private GameObject arrow;
    private GameObject playerSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arrow = transform.Find("directionArrow_0").gameObject;
        playerSprite = transform.Find("pacman_0").gameObject;
    }

    void Update()
    {
        GetInput();
        RotateArrow();
        Movement();
    }

    /// <summary>
    /// Handles the player's movement based on input.
    /// </summary>
    private void Movement()
    {
        Vector2 moveDirection = CalculateDirection();

        // Move the player in the direction of the input
        rb.linearVelocity = moveDirection * moveSpeed;

        if (moveDirection == Vector2.zero) return;

        // Flip the player sprite left to right and vice versa if the direction changes
        if ((moveDirection.x > 0 && !facingRight) || (moveDirection.x < 0 && facingRight))
        {
            FlipSprite(playerSprite, moveDirection);
            facingRight = !facingRight;
        }
        else if (moveDirection != Vector2.zero)
        {
            RotateSprite(playerSprite, moveDirection);
        }
    }

    /// <summary>
    /// Get input from the player.
    /// </summary>
    private void GetInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveX = moveX < 0 ? -1 : (moveX > 0 ? 1 : 0);
        moveY = moveY < 0 ? -1 : (moveY > 0 ? 1 : 0);

        if (moveX != 0 || moveY != 0)
        {
            inputDirection = new Vector2(moveX, moveY);
        }
    }

    /// <summary>
    /// Rotate the arrow based on input direction.
    /// </summary>
    private void RotateArrow()
    {
        if (inputDirection != Vector2.zero)
        {
            RotateSprite(arrow, inputDirection);
        }
    }

    /// <summary>
    /// Calculate the movement direction.
    /// </summary>
    private Vector2 CalculateDirection()
    {
        if (inputDirection != Vector2.zero && CanTurn(inputDirection))
        {
            lastMoveDirection = inputDirection;
        }

        return lastMoveDirection;
    }

    /// <summary>
    /// Flip the player sprite left to right and vice versa.
    /// </summary>
    private void FlipSprite(GameObject objectToFlip, Vector2 moveDirection)
    {
        Vector3 playerScale = objectToFlip.transform.localScale;
        playerScale.y *= -1;
        objectToFlip.transform.localScale = playerScale;
        RotateSprite(objectToFlip, moveDirection);
    }

    /// <summary>
    /// Rotate the player sprite
    /// </summary>
    private void RotateSprite(GameObject objectToRotate, Vector2 moveDirection)
    {
        Vector3 playerPosition = objectToRotate.transform.localPosition;
        if (moveDirection.y != 0)
        {
            playerPosition.y = Mathf.Max(Mathf.Abs(playerPosition.x), Mathf.Abs(playerPosition.y)) * moveDirection.y;
            playerPosition.x = 0;
        }
        else
        {
            playerPosition.x = Mathf.Max(Mathf.Abs(playerPosition.x), Mathf.Abs(playerPosition.y)) * moveDirection.x;
            playerPosition.y = 0;
        }
        objectToRotate.transform.localPosition = playerPosition;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>  
    /// Checks if the player can turn.  
    /// </summary>  
    private bool CanTurn(Vector2 direction)
    {
        BoxCollider2D boxCollider = playerSprite.GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size * 0.63f, 0, direction, turnDetectionDistance, LayerMask.GetMask("Obstacles"));
        return hit.collider == null;
    }
}
