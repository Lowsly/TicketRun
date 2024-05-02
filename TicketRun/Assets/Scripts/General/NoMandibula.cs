using UnityEngine;

public class NoMandibula : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            Destroy(other.gameObject);
        
    }
}
