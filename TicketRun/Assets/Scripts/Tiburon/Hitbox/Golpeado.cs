using UnityEngine;

public class Golpeado : MonoBehaviour
{
    public GameObject Splash, Shark; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Instantiate(Splash, Shark.transform.position, Quaternion.identity);
            Destroy(Shark);
        }
    }
}
