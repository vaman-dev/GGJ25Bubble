using UnityEngine;

public class PlaneController2D : MonoBehaviour
{
    public float speed = 5f;         // Speed of the plane
    public float rotationSpeed = 200f; // Rotation speed of the plane

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Forward movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        // Backward movement (disabled to prevent moving in the opposite direction)
        // Uncomment the following lines if you want to allow backward movement.
        // if (Input.GetKey(KeyCode.S))
        // {
        //     transform.Translate(-Vector2.right * speed * Time.deltaTime);
        // }

        // Rotate clockwise
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Rotate counter-clockwise
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
