using UnityEngine;

public class MusicAudioSourceG1 : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSettingsG1 audioSettings;

    private void Start()
    {
        audioSettings = AudioSettingsG1.audioSettings;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = audioSettings.GetMusicVolume();
        audioSettings.AddMusicAudioSource(audioSource);
    }

    private void OnDestroy()
    {
        audioSettings.RemoveMusicAudioSource(audioSource);
    }
}
