using System.Collections;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public LogoPart[] logoParts; // Array of all the parts of the XP logo

    private void Start()
    {
        StartCoroutine(AnimateLogoParts());
    }

    private IEnumerator AnimateLogoParts()
    {
        // Loop through each part of the logo
        foreach (LogoPart part in logoParts)
        {
            // Deactivate all parts first
            foreach (LogoPart otherPart in logoParts)
            {
                otherPart.gameObject.SetActive(false);
            }

            // Activate the current part
            part.gameObject.SetActive(true);
            
            // Start the animation for the current part
            part.StartAnimation();

            // Wait for the animation to complete before moving to the next part
            yield return new WaitForSeconds(3f); // Adjust delay based on animation time
        }

        // Optionally deactivate all parts after the animation sequence
        foreach (LogoPart part in logoParts)
        {
            part.gameObject.SetActive(false);
        }
    }
}
