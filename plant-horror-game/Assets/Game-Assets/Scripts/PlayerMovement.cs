using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 180f;
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;
    private Vector3 rotation;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f; // Reset vertical velocity if grounded and moving downwards
        }
        
        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        move = this.transform.TransformDirection(move);
        controller.Move(move * playerSpeed);
        this.transform.Rotate(this.rotation);

        // Get Input for movement
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // controller.Move(move * playerSpeed * Time.deltaTime);

        // Apply rotation based on movement direction
        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }

        // Handle Jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Apply Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
   
    }

}
