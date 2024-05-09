using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoJuego", menuName = "Juego")]
public class Juegos : ScriptableObject
{
   public GameObject juegoJugado;
   public Sprite imagen;

   public string nombre;
}
