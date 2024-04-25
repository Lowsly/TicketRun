using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    //public void Setup //

    public void RestartButton(string Scene){
        SceneManager.LoadScene(Scene);
    }

    public void ExitButton(){
        SceneManager.LoadScene("Menu");
    }
}
