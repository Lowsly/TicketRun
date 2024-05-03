using UnityEngine;

public class SFXAudioSourceG1 : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSettingsG1 audioSettings;

    void Start()
    {
        audioSettings = AudioSettingsG1.audioSettings;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = audioSettings.GetSFXVolume();
        audioSettings.AddSFXAudioSource(audioSource);
    }

    private void OnDestroy()
    {
        audioSettings.RemoveSFXAudioSource(audioSource);
    }
}
