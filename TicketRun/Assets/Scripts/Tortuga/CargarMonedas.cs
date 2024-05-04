using UnityEngine;
using TMPro;

public class CargarMonedas : MonoBehaviour
{
    public TextMeshProUGUI eggCounterText;
    public AudioClip eggCollectSound;
    private int eggsCollected = 0; 

    void Start()
    {
        LoadEggs();
        UpdateEggDisplay(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Huevo"))
        {
            Destroy(other.gameObject);
            eggsCollected += 1;
            SaveEggs();
            UpdateEggDisplay(); 
            PlayEggCollectSound();
        }
    }

    void PlayEggCollectSound()
        {
            if (eggCollectSound != null)
            {
        
                AudioSource.PlayClipAtPoint(eggCollectSound, transform.position);
            }
        }

    void SaveEggs()
    {
        PlayerPrefs.SetInt("EggsCollected", eggsCollected); 
        PlayerPrefs.Save(); 
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
