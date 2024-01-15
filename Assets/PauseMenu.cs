using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public static bool GameisPaused = false;

    public GameObject pauseMenuUi;
    public GameObject OptionsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Bind Escape to open Pause menu and resume
        {
            if (OptionsPanel.activeSelf)        // Check is OptionsPanel active
            {
                CloseOptionsPanel();            // Close OptionsPanel
                Pause();                // Open Pause Menu
            }
            else
            {
                TogglePause();
            }
        }
    }
    
    void TogglePause()
    {
        if (GameisPaused)
        {
            Resume(); //
        }
        else
        {
            Pause(); // 
        }
    }
    
    public void Resume() // Resumes game 
    {
        pauseMenuUi.SetActive(false);  // Disable pause menu
        OptionsPanel.SetActive(false); // Close OptionsPanel if opened
        Time.timeScale = 1f;            // Set the time scale to 1f
        GameisPaused = false;           // Disable pause state
    }
    void Pause()                            // Pauses game
    {
        pauseMenuUi.SetActive(true);        // Enable pause menu
        OptionsPanel.SetActive(false);      // Close OptionsPanel if opened
        Time.timeScale = 0f;                // Set the time scale to 0f
        GameisPaused = true;                //  Enable Pause State
    }
    public void LoadMenu() // 
    {
        Debug.Log("Loading Menu...");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting...");
    }

    public void MainMenu()
    {
            Time.timeScale = 1f; // Set the time scale to 1f
            SceneManager.LoadScene("MainMenu");
    }

    void CloseOptionsPanel() //Declare close options panel
    {
        OptionsPanel.SetActive(false); // Disable Options panel
    }


}