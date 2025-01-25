using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 position = transform.position;

        // Check for screen boundaries
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Bounce horizontally
        if (position.x > screenBounds.x || position.x < -screenBounds.x)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            position.x = Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x);
        }

        // Bounce vertically
        if (position.y > screenBounds.y || position.y < -screenBounds.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            position.y = Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y);
        }

        // Apply the corrected position
        transform.position = position;
    }
}
   