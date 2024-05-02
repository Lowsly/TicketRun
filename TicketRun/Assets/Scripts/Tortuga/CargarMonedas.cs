using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class CargarMonedas : MonoBehaviour
{
    public TextMeshProUGUI eggCounterText; // Reference to the TextMeshProUGUI component
    private int eggsCollected = 0; // Default value for eggs collected

    void Start()
    {
        LoadEggs(); // Load eggs collected from PlayerPrefs when the game starts
        UpdateEggDisplay(); // Update the TextMeshPro with the loaded value
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Huevo"))
        {
            Destroy(other.gameObject);
            eggsCollected += 1;
            SaveEggs();
            UpdateEggDisplay(); 
            
        }
    }

    void SaveEggs()
    {
        PlayerPrefs.SetInt("EggsCollected", eggsCollected); // Save the egg count to PlayerPrefs
        PlayerPrefs.Save(); // Make sure to save PlayerPrefs changes to disk
        Debug.Log("Eggs saved: " + eggsCollected);
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
