using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            Destroy(other.gameObject);
        else 
            other.transform.position = new Vector3(0, 0, 0);    
    }
}
