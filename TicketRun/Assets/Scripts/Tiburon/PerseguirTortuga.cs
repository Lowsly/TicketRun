using UnityEngine;

public class PerseguirTortuga : MonoBehaviour
{
    public Transform playerTransform; 
    public float speed = 0.8f;

    void Update()
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Calculate the angle to the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Move towards the player
        transform.position += direction * speed * Time.deltaTime;
    }
}
