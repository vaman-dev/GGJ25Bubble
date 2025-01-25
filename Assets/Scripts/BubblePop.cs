using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public AudioClip destroySound; // Assign the sound effect in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component to play the sound
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // 2D collision
    {
        // Check if the other GameObject has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Play the sound effect
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            Debug.Log($"{gameObject.name} collided with {other.gameObject.name}");

            // Destroy the GameObject after the sound finishes playing
            Destroy(gameObject, destroySound != null ? destroySound.length : 0);
        }
    }
}
