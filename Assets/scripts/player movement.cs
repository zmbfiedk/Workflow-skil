using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed of the player
    public float jumpHeight = 5f; // How high the player can jump
    public float groundDistance = 0.4f; // Distance to check if the player is grounded
    public LayerMask groundMask; // Mask to define what is considered the ground
    public Transform groundCheck; // Reference to the ground check object (empty GameObject beneath the player)

    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded; // Whether the player is grounded or not
    private float movementInputX;
    private float movementInputZ;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the player from rotating when moving
    }

    void Update()
    {
        // Read player input for horizontal (A/D or Left/Right Arrow) and vertical (W/S or Up/Down Arrow) movement
        movementInputX = Input.GetAxis("Horizontal");
        movementInputZ = Input.GetAxis("Vertical");

        // Check if player is grounded using a raycast (checking if player is on the ground)
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        // Handle jumping when player is grounded
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Handle movement in FixedUpdate to sync with physics engine
        MovePlayer();
    }

    void MovePlayer()
    {
        // Create movement vector (ignoring vertical movement for now)
        Vector3 movement = new Vector3(movementInputX, 0f, movementInputZ);
        movement = transform.TransformDirection(movement); // Convert local space to world space

        // Apply movement to Rigidbody (preserve current vertical speed)
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
    }

    void Jump()
    {
        // Reset Y velocity before applying jump to prevent upward speed overlap
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset Y velocity to ensure clean jump

        // Apply an upward force (impulse) for jumping
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}