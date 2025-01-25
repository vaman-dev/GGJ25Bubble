using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to spawn
    public float spawnInterval = 1f; // Interval between spawns
    public Vector2 throwForceRange = new Vector2(5f, 10f); // Force range for throwing
    public GameObject trigger; // Object used to start spawning
    public bool Started = false; // Flag to check if spawning has started

    private void Update()
    {
        // Start spawning objects if the trigger is null and it hasn't started yet
        if (!Started && trigger == null)
        {
            StartCoroutine(SpawnObjects());
            Started = true;
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the object at the spawner's position
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y);
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Apply a random throw force to the spawned object
            float throwForce = Random.Range(throwForceRange.x, throwForceRange.y);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(Random.Range(-1f, 1f), -throwForce);
            }
        }
    }
}
