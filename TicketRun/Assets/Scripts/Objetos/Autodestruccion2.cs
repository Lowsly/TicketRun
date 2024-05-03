using System.Collections;
using UnityEngine;

public class Autodestruccion2 : MonoBehaviour
{
   private SpriteRenderer _renderer;

   public int timer;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Self());
    }
    IEnumerator Self()
    {
        yield return new WaitForSecondsRealtime(timer);
        

        for (int i = 0; i < 6; i++)
            {
            float timeFlash = 0.1f - i/5;
             _renderer.color = new Color (0, 0, 0, 0f);
             yield return new WaitForSecondsRealtime(timeFlash);
             _renderer.color = Color.white;
             yield return new WaitForSecondsRealtime(timeFlash);
        }
        Destroy(this.gameObject);
        
    }
}
