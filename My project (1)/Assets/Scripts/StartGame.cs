using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    public GameObject startScreenUI; 
    

    // Start is called before the first frame update
    void Start()
    {
        
        startScreenUI.SetActive(true);  // Show the start menu

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call this method to resume the game
    public void ResumeGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Call this method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

        // If running in the Unity editor, stop play mode
      
    }
}