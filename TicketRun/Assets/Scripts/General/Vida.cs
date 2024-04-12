using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image hearts;
    
    private bool _lowHealth;

    private SpriteRenderer _renderer;


    static public bool _Immune;

    static public int health;

     void Start(){
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Hit()
    {   
        
        if (!_Immune){
            StartCoroutine(Immune());
            health -= 1;
            if(health>0)
                StartCoroutine(HeartColor());
            if(health>-1)
                StartCoroutine(Damaged());
            if (health == 0)
                _lowHealth = true;
                StartCoroutine(LowHealth());
            if (health <= -1){
                StartCoroutine(Damaged());
            } 
        }
    }


    IEnumerator HeartColor()
    {
            hearts.color = Color.red;
       yield return new WaitForSeconds(1f);
            hearts.color = Color.white;
    }

    IEnumerator Damaged()
    {
        for (int i = 0; i < 6; i++)
            {
             _renderer.enabled = false;
             yield return new WaitForSeconds(.1f);
             _renderer.enabled = true;
             yield return new WaitForSeconds(.1f);
            }
        
    }

    IEnumerator Immune() 
    {
        _Immune = true;
        yield return new WaitForSeconds(1f);
        _Immune = false;
    }
    IEnumerator LowHealth() 
    {
        while (_lowHealth == true){
                hearts.color = Color.red;
            yield return new WaitForSeconds(0.45f);
                hearts.color = Color.white;
            yield return new WaitForSeconds(0.45f);
        }
        yield return null;
    }
}
