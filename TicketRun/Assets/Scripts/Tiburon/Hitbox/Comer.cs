using UnityEngine;

public class Comer : MonoBehaviour
{
    public GameObject Splash; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basura"))
        {
            Instantiate(Splash, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
