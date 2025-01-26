using UnityEngine;

public class BigBubbleDestroyOnCollision : MonoBehaviour
{
    public AudioClip destroySound; // Assign the sound effect in the inspector
    private AudioSource audioSource;
    
    private static float lastDestructionTime = -10f; // Track the last destruction time
    public float destructionCooldown = 10f; // 10 seconds cooldown before destruction is allowed

    // Array of fellow big bubbles to track the last destruction time of other bubbles
    [SerializeField] private GameObject[] fellowBubbles;

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
            // Check if a bubble was destroyed in the last 10 seconds
            if (Time.time - lastDestructionTime < destructionCooldown)
            {
                Debug.Log("Cannot destroy this bubble because another bubble was destroyed recently.");
                return; // Exit early, do not destroy this bubble
            }

            // Check if any of the fellow big bubbles has been destroyed within the last 10 seconds
            foreach (var bubble in fellowBubbles)
            {
                if (bubble == null) continue;

                var otherBubbleScript = bubble.GetComponent<BigBubbleDestroyOnCollision>();
                if (otherBubbleScript != null && Time.time - otherBubbleScript.GetLastDestructionTime() < destructionCooldown)
                {
                    Debug.Log("Another fellow bubble was destroyed recently, preventing destruction.");
                    return; // Exit early if a fellow bubble was destroyed recently
                }
            }

            // Play the sound effect
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            // Destroy the GameObject after the sound finishes playing
            Destroy(gameObject, destroySound != null ? destroySound.length : 0);

            // Update the last destruction time
            lastDestructionTime = Time.time;
        }
    }

    // Method to get the last destruction time from other bubbles
    public float GetLastDestructionTime()
    {
        return lastDestructionTime;
    }
}
