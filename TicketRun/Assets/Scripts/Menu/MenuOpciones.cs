using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class MenuOpciones : MonoBehaviour
{
    public TextMeshProUGUI volumenTxt;
    [SerializeField] private AudioMixer audioMixer;
    public void CambiarVolumen(float volumen){
        audioMixer.SetFloat("Volumen", volumen);
    }     

}
