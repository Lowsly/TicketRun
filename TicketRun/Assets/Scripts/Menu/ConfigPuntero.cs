using UnityEngine;
using UnityEngine.UI;

public class ConfigPuntero : MonoBehaviour
{
    public Slider slider;
    public Player player;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("PointerDistance",0.6f);
    }

    
    public void SetDistance()
    {
       PlayerPrefs.SetFloat("PointerDistance",slider.value);
       player.setMouseDistancePos();
    }
}
