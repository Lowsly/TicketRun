using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSettingsG1 : MonoBehaviour
{
    
    public static AudioSettingsG1 audioSettings;

    [Header("Information - Read Only from inspector")]
    [SerializeField]
    private float musicVolume;
    [SerializeField]
    private float sfxVolume;

    float musicDefaultVolumeG1=0.7f;

    float sfxDefaultVolumeG1 = 0.9f;

    string musicAudioSTag ="Music-AudioSource";
    string sfxAudioSTag="SFX-AudioSource";

    string musicVolumeDName = "music-volume";
    string sfxVolumeDName = "sfx-volume";

    List<AudioSource> musicAudioSourcesGame1;
    List<AudioSource> sfxAudioSourcesGame1;

    [SerializeField]
    private int musicAudioSourcesCountG1=0;
    [SerializeField]
    private int sfxAudioSourcesCountG1 = 0;

    private void Awake()
    {
        audioSettings = this;
        musicAudioSourcesGame1 = new List<AudioSource>();
        sfxAudioSourcesGame1 = new List<AudioSource>();
        LoadSavedSettingsG1();
    }

    void LoadSavedSettingsG1()
    {
        musicVolume = PlayerPrefs.GetFloat(musicVolumeDName,musicDefaultVolumeG1);
        sfxVolume = PlayerPrefs.GetFloat(sfxVolumeDName, sfxDefaultVolumeG1);

    }

    public void ChangeMusicVolume (float newVolume)
    {
        musicVolume = newVolume;
        PlayerPrefs.SetFloat(musicVolumeDName, musicVolume);
        SetVolumeToAudioSources(musicAudioSourcesGame1, musicVolume);
    }

    public void ChangSFXVolume(float newVolume)
    {
        sfxVolume = newVolume;
        PlayerPrefs.SetFloat(sfxVolumeDName, sfxVolume);
        SetVolumeToAudioSources(sfxAudioSourcesGame1, sfxVolume);
    }

    void SetVolumeToAudioSources(List<AudioSource> audioSources, float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void AddMusicAudioSource(AudioSource audioSource)
    {
        musicAudioSourcesGame1.Add(audioSource);
        musicAudioSourcesCountG1 = musicAudioSourcesGame1.Count;
    }

    public void RemoveMusicAudioSource(AudioSource audioSource)
    {
        musicAudioSourcesGame1.Remove(audioSource);
        musicAudioSourcesCountG1 = musicAudioSourcesGame1.Count;
    }

    public void AddSFXAudioSource(AudioSource audioSource)
    {
        sfxAudioSourcesGame1.Add(audioSource);
        sfxAudioSourcesCountG1 = sfxAudioSourcesGame1.Count;
    }

    public void RemoveSFXAudioSource(AudioSource audioSource)
    {
        sfxAudioSourcesGame1.Remove(audioSource);
        sfxAudioSourcesCountG1 = sfxAudioSourcesGame1.Count;
    }

}
