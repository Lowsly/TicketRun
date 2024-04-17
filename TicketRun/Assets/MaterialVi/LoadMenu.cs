using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string menuSceneName = "NombreDeTuEscenaDelMenu";

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEndReached; // Suscribe el método OnVideoEndReached al evento loopPointReached del VideoPlayer
    }

    void OnVideoEndReached(VideoPlayer vp)
    {
        if (vp == videoPlayer)
        {
            LoadMenuScene();
        }
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName); // Carga la escena del menú
    }
}
