using UnityEngine;
using System;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 180f;
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;

    public bool sOneConsumed = false;
    public bool sTwoConsumed = false;
    private Vector3 rotation;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private int jumpCount = 0;
    private int maxJumpCount = 1;

    public GameObject eatPrompt;
    public GameObject interactPrompt;
    public GameObject glidePrompt;
    public GameObject dJumpPrompt;

    [Header("GlideSettings (specimen #2)")]
    [Tooltip("Multipler applied to gravity while gliding (0 = no gravity, 1, normal grav)")]
    public float glideGravMultiplier = 0.25f;
    [Tooltip ("Additional slight descent applied while glidinhg and moving forwards")]
    public float glideDescSpeed = -0.5f;
    public float glideForwardMultiplier = 1.3f;
    public float glidePropulsion = 6.0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f; 
        }

        // Handle rotation
        float horiz = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, horiz * rotationSpeed * Time.deltaTime, 0);

        // Handle movement (forward/back)
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.forward * vert;

        // Horizontal movement always applied
        Vector3 horizontalVelocity = move * playerSpeed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        // Update jump count if grounded
        maxJumpCount = sOneConsumed ? 2 : 1;
        if (groundedPlayer) jumpCount = 0;

        // Jump
        if (Input.GetButtonDown("Jump") && (groundedPlayer || jumpCount < maxJumpCount))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            jumpCount++;
        }

        // Determine gliding state
        bool isGliding = sTwoConsumed &&
                         Input.GetKey(KeyCode.LeftShift) &&
                         !groundedPlayer &&
                         playerVelocity.y < 0f;

        // Adjust gravity
        float appliedGrav = isGliding ? gravityValue * glideGravMultiplier : gravityValue;
        playerVelocity.y += appliedGrav * Time.deltaTime;

        // Forward propulsion WHILE gliding
        if (isGliding && Mathf.Abs(vert) > 0.1f)
        {
            Debug.Log("Gliding");
            playerVelocity.y += glideDescSpeed * Time.deltaTime;
            playerVelocity += transform.forward * glidePropulsion * Time.deltaTime;
        }
        //after gliding is done
        if (groundedPlayer || !isGliding)
        {
            playerVelocity.x = 0f;
            playerVelocity.z = 0f;
        }
        // Apply vertical + gliding velocity
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
