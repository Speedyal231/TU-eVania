using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Button Pressed");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f;
            // Show the pause menu
            pauseMenuUI.SetActive(true);
        }
        else
        {
            // Unpause the game
            Time.timeScale = 1f;
            // Hide the pause menu
            pauseMenuUI.SetActive(false);
        }
    }


    // You can call this method from a button click event to resume the game
    public void ResumeGame()
    {
        Debug.Log("TriggeredButton");
        TogglePause();
    }

    // Add other functions for your pause menu buttons as needed

    // Example: Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
