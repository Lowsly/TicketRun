using UnityEngine;

public class AjusteFondo : MonoBehaviour
{
    public float X, Y;
    private void Start()
    {
        float relacionDeAspectoPantalla = (float)Screen.width / Screen.height;

        
        float escala = relacionDeAspectoPantalla / 0.5f;
            
        this.gameObject.transform.localScale = new Vector3(escala*X, escala*Y, 1.0f);
    }
}