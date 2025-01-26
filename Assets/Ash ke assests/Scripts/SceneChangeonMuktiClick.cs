using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeonMuktiClick : MonoBehaviour
{
    [SerializeField] private GameObject TeleCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuktiClick()
    {
        SceneManager.LoadScene("BuggedWindows");
    }

    public void BubbleTon()
    {
        StartCoroutine(LoadingWithDelay());
        SceneManager.LoadScene("Antivirus");
    }

    private IEnumerator LoadingWithDelay()
    {
        TeleCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        TeleCanvas.SetActive(false);
    }
}
