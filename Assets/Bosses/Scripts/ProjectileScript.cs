using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the player here (e.g., reduce health)
            Debug.Log("Player hit by projectile!");
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}
