using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;
    public float scaleSpeed = 0.1f;
    public float collisionCheckDistance = 0.1f; // Distance to check for wall collisions
    public LayerMask wallLayer; // Layer for walls

    private bool isScaling = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isScaling = !isScaling; // Toggle Scale Mode
        }

        if (isScaling)
        {
            // Handle scaling
            HandleScaling();
        }
        else
        {
            // Handle movement and rotation
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        // Get input for movement (W/S for up/down, A/D for left/right)
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Check for collisions before moving
        if (!IsCollidingWithWall(moveDirection))
        {
            // Apply movement
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Calculate rotation based on input
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }
    }

    void HandleScaling()
    {
        float verticalInput = 0;
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.W)) verticalInput += scaleSpeed;
        if (Input.GetKey(KeyCode.S)) verticalInput -= scaleSpeed;
        if (Input.GetKey(KeyCode.A)) horizontalInput -= scaleSpeed;
        if (Input.GetKey(KeyCode.D)) horizontalInput += scaleSpeed;

        Vector3 newScale = transform.localScale;
        newScale.x += horizontalInput;
        newScale.y += verticalInput;

        // Prevent the scale from becoming negative
        newScale.x = Mathf.Max(newScale.x, 0.1f);
        newScale.y = Mathf.Max(newScale.y, 0.1f);

        transform.localScale = newScale;
    }

    bool IsCollidingWithWall(Vector3 moveDirection)
    {
        // Raycast in the direction of movement
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, collisionCheckDistance, wallLayer);

        // Return true if a wall is detected
        return hit.collider != null;
    }
}
