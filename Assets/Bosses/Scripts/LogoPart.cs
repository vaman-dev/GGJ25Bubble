using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LogoPart : MonoBehaviour
{
    public Transform player;                     // Reference to the player
    public GameObject projectilePrefab;          // Prefab for the projectile
    public float speed = 3f;                     // Movement speed for chasing the player
    public float firingInterval = 2f;            // Interval between firing projectiles
    public float projectileSpeed = 5f;           // Speed of the projectile

    private Rigidbody2D rb;
    private bool isAnimating = false;
    private bool isChasing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Start firing projectiles periodically
        InvokeRepeating(nameof(FireProjectile), firingInterval, firingInterval);
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            ChasePlayer();
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;
        // Trigger animation logic here (e.g., Animator trigger)
        Invoke(nameof(StartChasing), 2f); // Example: Start chasing after animation finishes
    }

    private void StartChasing()
    {
        isAnimating = false;
        isChasing = true;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void FireProjectile()
    {
        if (player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

            Vector2 direction = (player.position - transform.position).normalized;
            projRb.velocity = direction * projectileSpeed;

            Destroy(projectile, 5f); // Destroy projectile after 5 seconds
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with player (e.g., deal damage)
            Debug.Log("Hit Player!");
        }
    }
}
