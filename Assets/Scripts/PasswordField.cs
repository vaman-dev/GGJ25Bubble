using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class LoginManagerTMP : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField; // Reference to the TMP InputField
    [SerializeField] private Button loginButton; // Reference to the Login Button
    [SerializeField] private string correctPassword = "********"; // Set your correct password here
    [SerializeField] private ShakeObject objectToShake; // Reference to the object to shake (in Unity Inspector)

    void Start()
    {
        // Add a listener to the button
        loginButton.onClick.AddListener(CheckPassword);
    }

    private void CheckPassword()
    {
        string enteredPassword = passwordInputField.text;

        if (enteredPassword == correctPassword)
        {
            Debug.Log("Login Successful!");
            // Add your logic for successful login here
        }
        else
        {
            Debug.Log("Incorrect Password! Try again.");
            // Shake the object when the password is incorrect
            if (objectToShake != null)
            {
                objectToShake.StartShake();
            }
            else
            {
                Debug.LogError("ShakeObject reference is not assigned.");
            }
        }
    }
}
