using UnityEngine;
using TMPro;

public class CargarMonedas : MonoBehaviour
{
    public TextMeshProUGUI eggCounterText;
    public AudioClip eggCollectSound;
    private AudioSource audioSource;
    private int eggsCollected = 0; 
  
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        LoadEggs();
        UpdateEggDisplay(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Huevo"))
        {
            Destroy(other.gameObject);
            eggsCollected += 1;
            PlayerPrefs.SetInt("EggsCollected", eggsCollected); 
            PlayerPrefs.Save(); 
            UpdateEggDisplay(); 
            PlayEggCollectSound();
        }
    }

    void PlayEggCollectSound()
        {
            if (audioSource != null && eggCollectSound != null)
        {
            audioSource.PlayOneShot(eggCollectSound);
        }
        }

    void LoadEggs()
    {
        eggsCollected = PlayerPrefs.GetInt("EggsCollected", 0); 
    }

    void UpdateEggDisplay()
    {
        if (eggCounterText != null)
        {
            eggCounterText.text = "" + eggsCollected; 
        }
    }
}
