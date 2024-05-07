using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    public GameObject pantallaPausa; 
    public Button pauseButton; 
    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; 
            pantallaPausa.SetActive(true);
            if (pauseButton != null) pauseButton.interactable = false; 
        }
        else
        {
            Time.timeScale = 1f; 
            pantallaPausa.SetActive(false); 
            if (pauseButton != null) pauseButton.interactable = true; 
        }
    }

    public void LoadSelectedScene(string Scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Scene);
    }
}
