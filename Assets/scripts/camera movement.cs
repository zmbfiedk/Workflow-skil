using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody; // The body of the player (used for rotating the player horizontally)
    public float mouseSensitivity = 100000f; // Sensitivity of the mouse movement (horizontal and vertical)
    public float verticalRotationLimit = 100000f; // Limit the vertical camera rotation (up/down)

    private float xRotation = 0f; // Vertical rotation of the camera (up/down)
    private float yRotation = 0f; // Horizontal rotation of the camera (left/right)

    private void Start()
    {
        // Lock and hide the cursor when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input for both axes (X for horizontal, Y for vertical)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical mouse movement

        // Rotate the camera vertically (up/down)
        xRotation -= mouseY; // Invert the Y-axis for natural up/down movement
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit); // Clamp the vertical rotation

        // Apply the vertical rotation to the camera (pitch)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); // Rotate camera vertically (up/down)

        // Rotate the player body horizontally (left/right)
        yRotation += mouseX; // Add horizontal mouse movement to the current horizontal rotation
        playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f); // Rotate the player body horizontally (yaw)
    }
}