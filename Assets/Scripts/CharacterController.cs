using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;
    public float scaleSpeed = 0.1f;

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
            HandleRotation();
        }
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * moveInput * moveSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotateInput * rotateSpeed * Time.deltaTime);
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
}
