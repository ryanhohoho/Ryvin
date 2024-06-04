using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;      // The speed at which the player moves
    public float jumpForce = 5f;      // The force of the player's jump
    public float rotateSpeed = 2f;    // The speed at which the player rotates
    private Rigidbody rb;             // The Rigidbody component of the player
    private bool isJumping = false;   // A flag to keep track of whether the player is currently jumping
    public float maxSpeed = 10f; // The maximum speed at which the player can move

    void Start()
    {
        // Get the Rigidbody component of the player object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the forward and right vectors based on the player's rotation
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = transform.right;
        right.y = 0;
        right = right.normalized;

        // Determine the movement vector by combining the input, forward and right vectors
        Vector3 movement = (forward * moveZ + right * moveX) * moveSpeed;

        // Check if the player's current speed is greater than the max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            // If so, set the player's velocity to the max speed in the direction of the current velocity
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else
        {
            // Apply the movement force to the player's Rigidbody
            rb.AddForce(movement);
        }

        // Check for jump input and make sure the player isn't already jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Apply an upward force to the player's Rigidbody
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = true;
        }

        // *****************SAVE THIS CODE FOR LATER IT IS A NICE SCREENSHAKE FOR LIKE A PORTAL OPENING *******************
        //transform.Rotate(Vector3.up * 75 * rotateSpeed);
        // *****************SAVE THIS CODE FOR LATER IT IS A NICE SCREENSHAKE FOR LIKE A PORTAL OPENING *******************
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            // If so, set the isJumping flag to false
            isJumping = false;
        }
    }
}