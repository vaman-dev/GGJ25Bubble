using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class LoginManagerTMP : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField; // Reference to the TMP InputField
    [SerializeField] private Button loginButton; // Reference to the Login Button
    [SerializeField] private string correctPassword = "********"; // Set your correct password here

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
            // Add your logic for failed login here (e.g., show error message)
        }
    }
}
