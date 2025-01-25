using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class PasswordVideoManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField;  // Reference to the password input field
    [SerializeField] private VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
    [SerializeField] private string correctPassword = "password123";  // Correct password

    [SerializeField] private GameObject backgroundCanvas;  // Reference to the background canvas
    [SerializeField] private GameObject videoCanvas;  // Reference to the video player canvas
    [SerializeField] private Button passwordButton;  // Reference to the button that triggers the password check

    private void Start()
    {
        // Hide video canvas initially
        videoCanvas.SetActive(false);

        // Add listener to the password button
        if (passwordButton != null)
        {
            passwordButton.onClick.AddListener(CheckPasswordAndPlayVideo);
        }
        else
        {
            Debug.LogError("Password button is not assigned!");
        }
    }

    public void CheckPasswordAndPlayVideo()
    {
        string enteredPassword = passwordInputField.text;  // Get the entered password

        if (enteredPassword == correctPassword)
        {
            Debug.Log("Password is correct! Playing video...");

            // Hide the background canvas and show the video canvas
            backgroundCanvas.SetActive(false);
            videoCanvas.SetActive(true);

            // Play the video if the VideoPlayer is assigned
            if (videoPlayer != null)
            {
                // Load the video clip from Resources folder
                VideoClip videoClip = Resources.Load<VideoClip>("Videos/Videoplayback");  // Replace with your actual video name without extension

                if (videoClip != null)
                {
                    // Ensure VideoPlayer source is set to VideoClip
                    videoPlayer.source = VideoSource.VideoClip;
                    videoPlayer.clip = videoClip;  // Assign the loaded video clip to the VideoPlayer
                    videoPlayer.Play();  // Start playing the video
                    Debug.Log("Playing video...");
                }
                else
                {
                    Debug.LogError("Video file not found in Resources folder!");
                }
            }
            else
            {
                Debug.LogError("VideoPlayer component is not assigned!");
            }
        }
        else
        {
            Debug.Log("Incorrect password! Access denied.");
        }
    }
}
