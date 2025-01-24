using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlaneController2D : MonoBehaviour
{
    public float thrustForce = 5f;        // Force applied for forward movement
    public float rotationSpeed = 200f;   // Rotation speed of the plane
    public float constantSpeed = 5f;     // Constant speed in the x direction

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for the plane
    }

    void FixedUpdate()
    {
        HandleMovement();
        MaintainConstantSpeed();
    }

    private void HandleMovement()
    {
        // Apply thrust force in the forward direction (right side of the sprite)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.right * thrustForce);
        }

        // Apply thrust force in the backward direction (reverse movement)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.right * thrustForce);
        }

        // Rotate clockwise
        if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= rotationSpeed * Time.deltaTime;
        }

        // Rotate counter-clockwise
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += rotationSpeed * Time.deltaTime;
        }
    }

    private void MaintainConstantSpeed()
    {
        // Maintain constant velocity in the x direction
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = new Vector2(constantSpeed, currentVelocity.y);
    }
}
