using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    //public void Setup //

    public void RestartButton(){
        SceneManager.LoadScene("Juego2");
    }

    public void ExitButton(){
        SceneManager.LoadScene("Menu");
    }
}
