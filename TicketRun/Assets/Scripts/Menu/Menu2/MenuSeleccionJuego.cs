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
    private GameManager gameManager;

    private void Start(){
        gameManager = GameManager.Intance;
        
        index = PlayerPrefs.GetInt("JuegoIndex");

        if (index > gameManager.juegos.Count - 1){
            index = 0;
        }
        CambiarPantalla();

    }

    private void CambiarPantalla(){
        PlayerPrefs.SetInt("JuegoIndex", index);
        imagenJuego.sprite = gameManager.juegos[index].imagen;
        nombreJuego.text = gameManager.juegos[index].nombre;
    }

    public void SiguienteJuego(){
        if (index == gameManager.juegos.Count -1)
        {
            index = 0;
        }
        else{
            index += 1;

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
        CambiarPantalla();
    }

    public void IniciarJuego(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
