using UnityEngine;

public class Autodestruccion : MonoBehaviour
{
    void Start()
    {
        Invoke("AutoDestruir", 5f);
    }

    void AutoDestruir()
    {
        Destroy(gameObject);
    }
}
