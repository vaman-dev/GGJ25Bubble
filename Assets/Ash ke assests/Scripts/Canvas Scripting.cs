// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CanvasScripting : MonoBehaviour
// {
//     [SerializeField] private GameObject TeleCanvas;
//     [SerializeField] private GameObject WindowsCanvas;
//     public void Onclick(){
//         TeleCanvas.SetActive(true);
//         WindowsCanvas.SetActive(false);
//     }
// }

using System.Collections;
using UnityEngine;

public class CanvasScripting : MonoBehaviour
{
    [SerializeField] private GameObject TeleCanvas;
    [SerializeField] private GameObject WindowsCanvas;

    [SerializeField] private GameObject Tele;
    [SerializeField] private float delay = 2f; // Duration of the delay

    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] private AudioClip buttonClickSound; // Reference to the audio clip for the button click sound

    public void OnClick()
    {
        PlayButtonClickSound(); // Play the sound when the button is clicked
        StartCoroutine(SwitchCanvasWithDelay());
    }

    private IEnumerator SwitchCanvasWithDelay()
    {
        WindowsCanvas.SetActive(false);

        Tele.SetActive(true);

        yield return new WaitForSeconds(delay);

        Tele.SetActive(false);

        TeleCanvas.SetActive(true);
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.clip = buttonClickSound; // Assign the sound clip
            audioSource.Play(); // Play the sound
        }
        else
        {
            Debug.LogError("AudioSource or ButtonClickSound is not assigned!");
        }
    }
}
