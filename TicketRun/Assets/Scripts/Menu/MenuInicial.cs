using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
  
  public void Jugar(){
    SceneManager.LoadScene("Juego3");
  }

  public void Salir(){
    Application.Quit();
  }
}
