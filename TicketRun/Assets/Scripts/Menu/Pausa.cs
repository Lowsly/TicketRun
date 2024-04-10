using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading
using UnityEngine.UI; // Required for UI manipulation

public class Pausa : MonoBehaviour
{
    public GameObject pantallaPausa; // Assign this in the inspector
    public Button pauseButton; // Assign this in the inspector
    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pantallaPausa.SetActive(true); // Show the pause screen
            if (pauseButton != null) pauseButton.interactable = false; // Disable the pause button
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            pantallaPausa.SetActive(false); // Hide the pause screen
            if (pauseButton != null) pauseButton.interactable = true; // Re-enable the pause button
        }
    }

    public void LoadSelectedScene()
    {
        Time.timeScale = 1f; // Ensure the game's time scale is reset
        SceneManager.LoadScene("Menu");
    }
}
