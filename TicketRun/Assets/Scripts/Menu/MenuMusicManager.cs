using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            PlayMenuMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlayMenuMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
