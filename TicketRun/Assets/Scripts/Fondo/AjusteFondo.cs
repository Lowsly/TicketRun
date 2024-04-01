using UnityEngine;

public class AjusteFondo : MonoBehaviour
{
    public GameObject background;
    private void Start()
    {
        float relacionDeAspectoPantalla = (float)Screen.width / Screen.height;

        
        float escala = relacionDeAspectoPantalla / 0.5f;
            
        background.transform.localScale = new Vector3(escala*2, escala*6, 1.0f);
    }
}