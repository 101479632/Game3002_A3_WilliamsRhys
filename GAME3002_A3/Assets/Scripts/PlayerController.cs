using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float turnSpeed = 2f;

    [SerializeField] private float normalAcceleration = 25f;
    [SerializeField] private float normalDrag = 5f;

    [SerializeField] private PhysicMaterial normalMaterial;
    [SerializeField] private PhysicMaterial iceMaterial;
    [SerializeField] private PhysicMaterial bounceMaterial;

    [SerializeField] private float iceDrag = 0.5f;

    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;

    private Collider playerCollider;

    [SerializeField] private int maxJumps = 2;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isOnIce;
    private int jumpCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;

        isGrounded = false;
        isOnIce = false;
        bool isOnBounce = false;

        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Ground")) isGrounded = true;
            if (hit.CompareTag("Ice"))
            {
                isGrounded = true;
                isOnIce = true;
            }
            if (hit.CompareTag("Bounce"))
            {
                isGrounded = true;
                isOnBounce = true;
            }
        }

        // Apply appropriate material
        if (playerCollider != null)
        {
            if (isOnBounce)
                playerCollider.material = bounceMaterial;
            else if (isOnIce)
                playerCollider.material = iceMaterial;
            else
                playerCollider.material = normalMaterial;
        }

        // Only reset jumps if we were not grounded last frame
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }

        rb.drag = isOnIce ? iceDrag : normalDrag;

        wasGrounded = isGrounded; // update state for next frame
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0f)
        {
            if (isOnIce)
            {
                Vector3 force = moveInput * moveForce;
                rb.AddForce(force, ForceMode.Acceleration);
            }
            else
            {
                Vector3 desiredVelocity = moveInput * maxSpeed;
                Vector3 velocityChange = desiredVelocity - rb.velocity;
                velocityChange.y = 0f;
                rb.AddForce(velocityChange * normalAcceleration, ForceMode.Acceleration);
            }
        }

        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 clamped = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
