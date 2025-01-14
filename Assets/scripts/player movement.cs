using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    public float speed = 5f; // Movement speed of the player
     float runspeed;
    public float jumpHeight = 5f; // How high the player can jump
     float groundDistance = 1.4f; // Distance to check if the player is grounded
    public LayerMask groundMask; // Mask to define what is considered the ground
    public Transform groundCheck; // Reference to the ground check object (empty GameObject beneath the player)
=======
    // Spawn Variables
    public float playerSetX;
    public float playerSetY;
    public float playerSetZ;
>>>>>>> 8a8691dbce62987c6ed45adc378408c32734c8ee

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
<<<<<<< HEAD
        rb.freezeRotation = true; // Prevent the player from rotating when moving
        runspeed = speed * 1.5f;
=======

        // Player Spawn Point (set position at the beginning)
        transform.position = new Vector3(playerSetX, playerSetY, playerSetZ);

        // Optional: Reference the main camera if needed for movement direction
        if (!playerCamera)
        {
            playerCamera = Camera.main; // Use the main camera by default
        }
>>>>>>> 8a8691dbce62987c6ed45adc378408c32734c8ee
    }

    void Update()
    {
<<<<<<< HEAD
        // Read player input for horizontal (A/D or Left/Right Arrow) and vertical (W/S or Up/Down Arrow) movement
        movementInputX = Input.GetAxis("Horizontal");
        movementInputZ = Input.GetAxis("Vertical");

        // Check if player is grounded using a raycast (checking if player is on the ground)
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        // Handle jumping when player is grounded
        Debug.DrawLine(groundCheck.position, Vector3.down, Color.red);

        Debug.Log(isGrounded);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Handle movement in FixedUpdate to sync with physics engine
=======
        // Player Movement (WASD or Arrow Keys)
>>>>>>> 8a8691dbce62987c6ed45adc378408c32734c8ee
        MovePlayer();
    }

    void MovePlayer()
    {
<<<<<<< HEAD
        // Create movement vector (ignoring vertical movement for now)
        Vector3 movement = new Vector3(movementInputX, 0f, movementInputZ);
        movement = transform.TransformDirection(movement); // Convert local space to world space
        float currentspeed = speed;



        if (Input.GetKey(KeyCode.LeftShift)) // sprint and sprint speed
        {
            currentspeed = runspeed;
            //Debug.Log(currentspeed);
        }
        // Apply movement to Rigidbody (preserve current vertical speed)
        rb.velocity = new Vector3(movement.x * currentspeed, rb.velocity.y, movement.z * currentspeed);
=======
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
>>>>>>> 8a8691dbce62987c6ed45adc378408c32734c8ee
    }

    void Jump()
    {
<<<<<<< HEAD
        // Reset Y velocity before applying jump to prevent upward speed overlap
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset Y velocity to ensure clean jump

        // Apply an upward force (impulse) for jumping
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

        Debug.Log("jump");

=======
        // Apply a force to simulate jumping (only if the player is on the ground)
        if (Mathf.Abs(rb.velocity.y) < 0.01f) // Check if player is grounded
        {
            rb.AddForce(Vector3.up * playerJumpHeight, ForceMode.Impulse);
        }
>>>>>>> 8a8691dbce62987c6ed45adc378408c32734c8ee
    }
}
