using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class MenuOpciones : MonoBehaviour
{
    public GameObject check, cross;
    public TextMeshProUGUI volumenTxt;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        setJoystick();
    }
    public void CambiarVolumen(float volumen){
        audioMixer.SetFloat("Volumen", volumen);
    }     
    public void activarJoystick()
    {
        
        if(PlayerPrefs.GetInt("joystickEnabled", 0) == 0)
        {
            PlayerPrefs.SetInt("joystickEnabled",1);
            setJoystick();
        }
        else if(PlayerPrefs.GetInt("joystickEnabled", 0) == 1)
            PlayerPrefs.SetInt("joystickEnabled",0);
            setJoystick();

    }
    void setJoystick()
    {
        if(check !=null && cross != null)
        {
            if (PlayerPrefs.GetInt("joystickEnabled",0) == 0)
            {
                check.SetActive(false);
                cross.SetActive(true);
            }
            else  if (PlayerPrefs.GetInt("joystickEnabled") == 1)
            {
                check.SetActive(true);
                cross.SetActive(false);
            }
        }
        
    }

}
