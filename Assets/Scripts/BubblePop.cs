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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other GameObject has the tag "data"
        if (other.CompareTag("Player"))
        {
            // Play the sound effect
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            // Destroy the other GameObject after the sound finishes playing
            Destroy(other.gameObject, destroySound.length);
        }
    }
}
