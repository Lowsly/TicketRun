using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionJuego : MonoBehaviour
{

    private int index;

    [SerializeField] private Image imagenJuego;
    [SerializeField] private TextMeshProUGUI nombreJuego;

    [SerializeField] private TextMeshProUGUI descripcionJuego;
    private GameManager gameManager;
    private AudioSource audioSource;
    public AudioClip NextGame, BackGame;

    private void Start(){
        gameManager = GameManager.Intance;
        
        index = PlayerPrefs.GetInt("JuegoIndex");

        if (index > gameManager.juegos.Count - 1){
            index = 0;
        }
        CambiarPantalla();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    private void CambiarPantalla(){
        PlayerPrefs.SetInt("JuegoIndex", index);
        imagenJuego.sprite = gameManager.juegos[index].imagen;
        nombreJuego.text = gameManager.juegos[index].nombre;
        descripcionJuego.text = gameManager.juegos[index].descripcion;
    }

    public void SiguienteJuego(){
        if (index == gameManager.juegos.Count -1)
        {
            index = 0;
        }
        else{
            index += 1;

        }
        if (NextGame != null)
        {
            audioSource.PlayOneShot(NextGame);
        }
        CambiarPantalla();

    }

       public void AnteriorJuego(){
        if (index == 0)
        {
            index = gameManager.juegos.Count -1;
        }
        else{
            index -= 1;

        }
        if (BackGame != null)
        {
            audioSource.PlayOneShot(BackGame);
        }
        CambiarPantalla();
    }

    public void IniciarJuego(){
        string nombreEscena = gameManager.juegos[index].nombreEscena;
        SceneManager.LoadScene(nombreEscena);
    }
}
