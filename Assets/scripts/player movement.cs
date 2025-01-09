using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Spawn Variables
    public float playerSetX;
    public float playerSetY;
    public float playerSetZ;

    // Movement Variables
    public float moveSpeed = 5f;        // Speed of movement
    public float playerJumpHeight = 2f; // Jump height
    private Rigidbody rb;

    // Camera reference (optional for forward movement direction)
    public Camera playerCamera;

    void Start()
    {
        // Get the Rigidbody component for physics-based movement
        rb = GetComponent<Rigidbody>();

        // Player Spawn Point (set position at the beginning)
        transform.position = new Vector3(playerSetX, playerSetY, playerSetZ);

        // Optional: Reference the main camera if needed for movement direction
        if (!playerCamera)
        {
            playerCamera = Camera.main; // Use the main camera by default
        }
    }

    void Update()
    {
        // Player Movement (WASD or Arrow Keys)
        MovePlayer();
    }

    void MovePlayer()
    {
        // Get input from keyboard (WASD or Arrow Keys)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Get camera forward and right directions
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Make sure we ignore the Y component of the vectors (we don't want vertical movement from the camera's tilt)
        forward.y = 0f;
        right.y = 0f;

        // Normalize the directions to avoid faster diagonal movement
        forward.Normalize();
        right.Normalize();

        // Calculate movement vector relative to camera's view direction
        Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        // Apply movement to the Rigidbody (physics-based)
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

        // Jumping (only allow jumping if on the ground)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply a force to simulate jumping (only if the player is on the ground)
        if (Mathf.Abs(rb.velocity.y) < 0.01f) // Check if player is grounded
        {
            rb.AddForce(Vector3.up * playerJumpHeight, ForceMode.Impulse);
        }
    }
}
