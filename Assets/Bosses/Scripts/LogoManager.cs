using System.Collections;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public LogoPart[] logoParts; // Array of all the parts of the XP logo
    public Transform player; // Reference to the player

    private void Start()
    {
        foreach (LogoPart part in logoParts)
        {
            part.player = player; // Assign the player reference
        }
        StartCoroutine(AnimateLogoParts());
    }

    private IEnumerator AnimateLogoParts()
    {
        foreach (LogoPart part in logoParts)
        {
            // Deactivate all other parts
            foreach (LogoPart otherPart in logoParts)
            {
                otherPart.gameObject.SetActive(false);
            }

            // Activate the current part and start its animation
            part.gameObject.SetActive(true);
            part.StartAnimation();

            // Wait until the current logo part is destroyed
            while (!part.IsDestroyed)
            {
                yield return null; // Wait until the condition is met
            }
        }
    }
}
