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

        //update for specimen 2 consumption
        maxJumpCount = sOneConsumed ?2:1;

        if (groundedPlayer)
        {
            jumpCount = 0; // Reset jump count when grounded
        }
        // Handle Jumping
        if (Input.GetButtonDown("Jump") && (groundedPlayer || jumpCount < maxJumpCount))
        {

            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumpCount++; 
        }

        // Apply Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        //Specimen 2 glide effect

        bool isGliding = sTwoConsumed && Input.GetKey(KeyCode.LeftShift) && !groundedPlayer && playerVelocity.y <0f;

        float appliedGrav = isGliding ? gravityValue * glideGravMultiplier : gravityValue;

        playerVelocity.y += appliedGrav * Time.deltaTime;

        Vector3 propulsion = Vector3.zero;
        //&& (Math.Abs(Input.GetAxisRaw("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f)
        if(isGliding && (Math.Abs(Input.GetAxisRaw("Vertical")) > 0.1f))
        {   
            Debug.Log("Gliding");
            playerVelocity.y += glideDescSpeed * Time.deltaTime;
            propulsion = this.transform.forward *glidePropulsion;
        }
        controller.Move((playerVelocity+propulsion) * Time.deltaTime);
   
    }

}
