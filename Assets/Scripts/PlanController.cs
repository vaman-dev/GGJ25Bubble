using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlaneController2D : MonoBehaviour
{
    public int planeIndex;                 // Unique index for each plane
    public float forwardSpeed = 5f;        // Constant forward speed in the local X direction
    public float rotationSpeed = 200f;     // Rotation speed of the plane
    public float gravityForce = 9.81f;     // Gravity force to pull the plane downwards
    public Sprite destroyedSprite;         // Sprite to display when the plane is destroyed
    public AudioClip destroySound;         // Sound effect to play when the plane is destroyed
    public float bottomOffset = 0.5f;        // Offset from the bottom of the screen in world units

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isDestroyed = false;
    private Vector2 screenBounds;
    private bool isControlEnabled = false;

    private static int currentPlaneIndex = 0; // Tracks the active plane index
    private static PlaneController2D[] allPlanes; // Cache all planes for better performance

    private void Awake()
    {
        // Ensure required components are attached
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Debug.LogWarning($"SpriteRenderer was missing and has been added to {gameObject.name}");
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        rb.gravityScale = 0; // Disable default gravity
        rb.velocity = transform.right * forwardSpeed; // Set initial velocity

        // Calculate screen bounds in world space
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Cache all planes on the first plane's Start call
        if (allPlanes == null || allPlanes.Length == 0)
        {
            allPlanes = FindObjectsOfType<PlaneController2D>();
        }

        // Enable control for the active plane
        EnableControl(planeIndex == currentPlaneIndex);
    }

    private void Update()
    {
        if (isControlEnabled && !isDestroyed)
        {
            HandleRotation();
            HandleYAxisFlip();
            KeepPlaneOnScreen();
        }
    }

    private void FixedUpdate()
    {
        if (isControlEnabled && !isDestroyed)
        {
            ApplyGravity();
        }
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector2.down * gravityForce);
    }

    private void HandleRotation()
    {
        float rotationInput = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = -1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = 1;
        }

        transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);

        // Maintain constant forward velocity
        if (rb != null)
        {
            rb.velocity = transform.right * forwardSpeed;
        }
    }

    private void HandleYAxisFlip()
    {
        float zAngle = transform.eulerAngles.z;
        if (zAngle > 180) zAngle -= 360;

        Vector3 localScale = transform.localScale;
        localScale.y = Mathf.Abs(localScale.y) * (Mathf.Abs(zAngle) > 90 ? -1 : 1);
        transform.localScale = localScale;
    }

    private void KeepPlaneOnScreen()
    {
        Vector3 position = transform.position;

        // Apply the bottom offset
        float minYWithOffset = -screenBounds.y + bottomOffset;

        // Destroy the plane if it goes off-screen, considering the offset
        if (position.x < -screenBounds.x || position.x > screenBounds.x || position.y < minYWithOffset || position.y > screenBounds.y)
        {
            TriggerDestruction();
        }
    }

    private void TriggerDestruction()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        // Replace the sprite with the destroyed version
        if (destroyedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = destroyedSprite;

            Vector3 originalScale = transform.localScale;
            Vector2 originalSize = spriteRenderer.sprite.bounds.size;
            Vector2 destroyedSize = destroyedSprite.bounds.size;

            float scaleX = originalScale.x * (originalSize.x / destroyedSize.x);
            float scaleY = originalScale.y * (originalSize.y / destroyedSize.y);

            transform.localScale = new Vector3(scaleX, scaleY, originalScale.z);
        }

        // Play destruction sound
        if (destroySound != null)
        {
            audioSource.PlayOneShot(destroySound);
        }

        rb.velocity = Vector2.zero;
        rb.gravityScale = 2f; // Make it fall faster
        rb.angularVelocity = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Destroy(gameObject, 3f);

        // Move to the next plane
        if (planeIndex == currentPlaneIndex)
        {
            Invoke(nameof(EnableNextPlane), 0.5f);
        }
    }

    private void EnableNextPlane()
    {
        if (allPlanes == null || allPlanes.Length == 0)
            return;

        // Skip destroyed planes
        do
        {
            currentPlaneIndex++;
        } while (currentPlaneIndex < allPlanes.Length && allPlanes[currentPlaneIndex] == null);

        // Enable the next plane if it exists
        if (currentPlaneIndex < allPlanes.Length && allPlanes[currentPlaneIndex] != null)
        {
            allPlanes[currentPlaneIndex].EnableControl(true);
        }
    }

    public void EnableControl(bool enable)
    {
        if (rb == null) return; // Safeguard against accessing a destroyed Rigidbody2D

        isControlEnabled = enable;

        if (!enable)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        else
        {
            rb.velocity = transform.right * forwardSpeed;
        }
    }
}
