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

    public void OnClick()
    {
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
}
