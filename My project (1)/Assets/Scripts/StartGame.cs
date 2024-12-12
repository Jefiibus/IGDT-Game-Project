using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    public GameObject startScreenUI;
    private FadeToWhite fadeToWhiteScript;
    private GameObject image;
    public GameObject controls;
    // Start is called before the first frame update
    void Start()
    {
        startScreenUI.SetActive(true);  // Show the start menu
        fadeToWhiteScript = GameObject.Find("FadeToWhite").GetComponent<FadeToWhite>();
        image = GameObject.Find("FadeToWhite");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call this method to resume the game
    public void ResumeGame()
    {

        StartCoroutine(FadeToWhite());
    }
    IEnumerator FadeToWhite()
    {
        fadeToWhiteScript.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
    // Call this method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
        // If running in the Unity editor, stop play mode
    }
    public void Controls()
    {
        startScreenUI.SetActive(false);
        controls.SetActive(true);
    }
    public void Return()
    {
        controls.SetActive(false);
        startScreenUI.SetActive(true);
    }
}