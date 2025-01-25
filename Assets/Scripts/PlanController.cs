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
    public float bottomOffset = 0.5f;      // Offset from the bottom of the screen in world units

    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isDestroyed = false;
    private bool isControlEnabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        rb.gravityScale = 0; // Disable gravity at the start
        rb.velocity = Vector2.zero; // Set initial velocity to 0
    }

    private void Update()
    {
        if (isControlEnabled && !isDestroyed)
        {
            HandleRotation();
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
        rb.velocity = transform.right * forwardSpeed;
    }

    public void EnableControl(bool enable)
    {
        isControlEnabled = enable;
        if (!enable)
        {
            // Stop movement when control is disabled
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        else
        {
            // Activate movement when control is enabled
            rb.velocity = transform.right * forwardSpeed;
            rb.gravityScale = 1; // Enable gravity
        }
    }

    private void OnBecameInvisible()
    {
        if (!isDestroyed && isControlEnabled)
        {
            TriggerDestruction();
        }
    }

    private void TriggerDestruction()
    {
        isDestroyed = true;

        // Replace the sprite with the destroyed version
        if (destroyedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = destroyedSprite;
        }

        // Play destruction sound
        if (destroySound != null)
        {
            audioSource.PlayOneShot(destroySound);
        }

        rb.velocity = Vector2.zero;
        rb.gravityScale = 2f;

        Destroy(gameObject, 3f);

        // Notify PlaneManager to activate the next plane
        PlaneManager.Instance.ActivateNextPlane();
    }
}
