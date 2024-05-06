using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteSound : MonoBehaviour
{
    public AudioClip biteSound;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayBiteSoundCoroutine());
    }

    IEnumerator PlayBiteSoundCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            PlayBiteSound();
        }
    }

    void PlayBiteSound()
    {
        if (biteSound != null)
        {
            audioSource.PlayOneShot(biteSound);
        }
    }
}
