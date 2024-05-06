using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    public GameObject pantallaPausa, joystick; 
    public Button pauseButton; 
    private bool isPaused = false;

    void Awake()
    {
        if(PlayerPrefs.GetInt("joystickEnabled") == 0)
            {
                joystick.SetActive(false); 
            }
            if(PlayerPrefs.GetInt("joystickEnabled") == 1)
            {
                joystick.SetActive(true); 
            }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; 
            pantallaPausa.SetActive(true);
            joystick.SetActive(false); 
            if (pauseButton != null) pauseButton.interactable = false; 
        }
        else
        {
            Time.timeScale = 1f; 
            pantallaPausa.SetActive(false); 
            if(PlayerPrefs.GetInt("joystickEnabled", 0) == 0)
            {
                joystick.SetActive(false); 
            }
            if(PlayerPrefs.GetInt("joystickEnabled", 0) == 1)
            {
                joystick.SetActive(true); 
            }
            if (pauseButton != null) pauseButton.interactable = true; 
        }
    }

    public void LoadSelectedScene(string Scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Scene);
    }
}
