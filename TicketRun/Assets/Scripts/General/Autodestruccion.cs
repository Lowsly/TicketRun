using UnityEngine;

public class Autodestruccion : MonoBehaviour
{
    public float Tiempo;
    void Start()
    {
        Invoke("AutoDestruir", Tiempo);
    }

    void AutoDestruir()
    {
        Destroy(gameObject);
    }
}
