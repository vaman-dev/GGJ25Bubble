using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlaneController2D : MonoBehaviour
{
    public float forwardSpeed = 5f;      // Constant forward speed in the local X direction
    public float rotationSpeed = 200f;  // Rotation speed of the plane
    public float gravityForce = 9.81f;  // Gravity force to pull the plane downwards

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable default gravity
        rb.velocity = transform.right * forwardSpeed; // Set initial velocity in the local X direction
    }

    void Update()
    {
        HandleRotation();
        HandleYAxisFlip();
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        // Apply gravity as a downward force on the Y-axis
        rb.AddForce(Vector2.down * gravityForce);
    }

    private void HandleRotation()
    {
        // Rotate clockwise around Z-axis
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        // Rotate counter-clockwise around Z-axis
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        // Maintain forward velocity in the local X direction relative to the plane's rotation
        rb.velocity = transform.right * forwardSpeed;
    }

    private void HandleYAxisFlip()
    {
        // Get the current Z-axis rotation (0 to 360 degrees)
        float zAngle = transform.eulerAngles.z;

        // Normalize angle to (-180 to 180) range
        if (zAngle > 180)
        {
            zAngle -= 360;
        }

        // Flip Y-axis if Z-angle exceeds 90 degrees in either direction
        if (Mathf.Abs(zAngle) > 90)
        {
            Vector3 localScale = transform.localScale;
            localScale.y = -Mathf.Abs(localScale.y); // Ensure Y is negative
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.y = Mathf.Abs(localScale.y); // Ensure Y is positive
            transform.localScale = localScale;
        }
    }
}
