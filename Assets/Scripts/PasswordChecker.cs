using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    public Button checkPasswordButton;
    private string correctPassword = "********";

    void Start()
    {
        // Assign the OnCheckPassword method to the button's onClick event
        checkPasswordButton.onClick.AddListener(OnCheckPassword);
    }

    public void OnCheckPassword()
    {
        // Get the input password from the input field
        string inputPassword = passwordInputField.text;

        // Check if the input password matches the correct password
        if (inputPassword == correctPassword)
        {
            Debug.Log("Access Granted");
        }
        else
        {
            Debug.Log("Access Denied");
        }
    }
}
