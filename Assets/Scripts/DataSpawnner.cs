using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to spawn
    public float spawnInterval = 1f; // Interval between spawns
    public Vector2 spawnRange = new Vector2(-8f, 8f); // Horizontal spawn range
    public Vector2 throwForceRange = new Vector2(5f, 10f); // Force range for throwing

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()   
    {
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the object at a random position
            float spawnX = Random.Range(spawnRange.x, spawnRange.y);
            Vector2 spawnPosition = new Vector2(spawnX, transform.position.y);
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Apply a random throw force
            float throwForce = Random.Range(throwForceRange.x, throwForceRange.y);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(Random.Range(-1f, 1f), -throwForce);
            }
        }
    }
}
