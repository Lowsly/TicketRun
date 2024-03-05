using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 5f; // Time in seconds before the GameObject is destroyed

    void Start()
    {
        // Invoke the DestroyObject method after the specified delay
        Invoke("DestroyObject", destroyTime);
    }

    void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
    public float moveSpeed = 5f; // Speed at which the object moves to the left

    void Update()
    {
        // Move the object to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
