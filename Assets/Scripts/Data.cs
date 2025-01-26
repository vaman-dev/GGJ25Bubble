using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Random Velocity Range")]
    public Vector2 xVelocityRange = new Vector2(-5f, 5f); // Range for horizontal velocity
    public Vector2 yVelocityRange = new Vector2(-5f, 5f); // Range for vertical velocity

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable gravity to ensure constant velocity
        rb.gravityScale = 0;

        // Assign a random initial velocity
        float randomXVelocity = Random.Range(xVelocityRange.x, xVelocityRange.y);
        float randomYVelocity = Random.Range(yVelocityRange.x, yVelocityRange.y);
        rb.velocity = new Vector2(randomXVelocity, randomYVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the velocity based on the collision normal
        Vector2 normal = collision.contacts[0].normal;
        rb.velocity = Vector2.Reflect(rb.velocity, normal);
    }

    private void Update()
    {
        Vector2 position = transform.position;

        // Keep the object within the screen bounds
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, -screenBounds.x + Mathf.Abs(transform.localScale.x), screenBounds.x - Mathf.Abs(transform.localScale.x));
        position.y = Mathf.Clamp(position.y, -screenBounds.y + Mathf.Abs(transform.localScale.y), screenBounds.y - Mathf.Abs(transform.localScale.y));

        transform.position = position;
    }
}
