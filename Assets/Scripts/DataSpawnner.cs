using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to spawn (small bubbles or chips)
    public GameObject[] bigBubbles; // Array of big bubbles to manage
    public int maxSpawnCount = 3; // Maximum number of objects to spawn
    public GameObject trigger; // Object used to start spawning

    private bool hasStartedSpawning = false; // Flag to check if spawning has started

    private void Start()
    {
        // Ensure big bubbles are assigned
        if (bigBubbles == null || bigBubbles.Length == 0)
        {
            Debug.LogError("Big bubbles array is empty or not assigned!");
            return;
        }

        // Disable all big bubbles' colliders initially
        foreach (GameObject bigBubble in bigBubbles)
        {
            if (bigBubble != null)
            {
                BoxCollider2D collider = bigBubble.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        // Start spawning objects if the trigger is null and it hasn't started yet
        if (!hasStartedSpawning && trigger == null)
        {
            SpawnAllObjects();
            hasStartedSpawning = true;
        }

        // Manage the colliders of big bubbles dynamically based on the presence of "Chip" objects
        ManageBigBubbleColliders();
    }

    // Spawn all objects (chips) instantly
    private void SpawnAllObjects()
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            // Spawn the object at the spawner's position
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Set the tag for the spawned object to "Chip"
            spawnedObject.tag = "Chip";

            // Apply a random throw force to the spawned object
            float throwForce = Random.Range(5f, 10f); // Example values
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(Random.Range(-1f, 1f), -throwForce);
            }
        }
    }

    // Dynamically manage the colliders of big bubbles based on the presence of "Chip" objects
    private void ManageBigBubbleColliders()
    {
        // Check if any active objects in the scene have the tag "Chip"
        GameObject[] chips = GameObject.FindGameObjectsWithTag("Chip");
        bool hasActiveChips = chips.Length > 0;

        // Enable or disable the colliders on big bubbles based on the presence of "Chip" objects
        foreach (GameObject bigBubble in bigBubbles)
        {
            if (bigBubble != null)
            {
                BoxCollider2D collider = bigBubble.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = !hasActiveChips; // Disable colliders if there are active "Chip" objects
                }
            }
        }
    }
}
