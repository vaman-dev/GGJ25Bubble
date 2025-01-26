using UnityEngine;

public class CountPlayers : MonoBehaviour
{
    public GameObject targetObject;  // Reference to the GameObject to enable when player count is 0

    void Update()
    {
        // Find all game objects with the tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Log the count of players
        Debug.Log("Number of 'Player' objects in the scene: " + players.Length);

        // Check if the player count is 0
        if (players.Length == 0)
        {
            // Enable the target object if there are no players
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        }
    }
}
