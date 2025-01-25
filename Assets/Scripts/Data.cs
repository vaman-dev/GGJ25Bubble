using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Random Velocity Range")]
    public Vector2 xVelocityRange = new Vector2(-5f, 5f); // Range for horizontal velocity
    public Vector2 yVelocityRange = new Vector2(-5f, 5f); // Range for vertical velocity

    private Vector2 screenBounds; // Screen bounds in world coordinates
    private float objectWidth;    // Half the width of the object
    private float objectHeight;   // Half the height of the object

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Assign a random initial velocity
        float randomXVelocity = Random.Range(xVelocityRange.x, xVelocityRange.y);
        float randomYVelocity = Random.Range(yVelocityRange.x, yVelocityRange.y);
        rb.velocity = new Vector2(randomXVelocity, randomYVelocity);

        // Calculate screen bounds in world coordinates
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Calculate half the size of the object's sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;  // Half the width
            objectHeight = sr.bounds.extents.y; // Half the height
        }
    }

    private void Update()
    {
        Vector2 position = transform.position;

        // Bounce horizontally
        if (position.x + objectWidth > screenBounds.x || position.x - objectWidth < -screenBounds.x)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            position.x = Mathf.Clamp(position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        }

        // Bounce vertically
        if (position.y + objectHeight > screenBounds.y || position.y - objectHeight < -screenBounds.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            position.y = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        }

        // Apply the corrected position
        transform.position = position;
    }
}
