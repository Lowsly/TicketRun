using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager Intance;
    public List<Juegos> juegos;

    private void Awake(){
        if(GameManager.Intance == null){
            GameManager.Intance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
