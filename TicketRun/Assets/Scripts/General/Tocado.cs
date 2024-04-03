using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Tocado : MonoBehaviour
{
    public float swipeStrength = 5f; // Adjust this value to change how far objects are swiped away

    private Rigidbody2D rb;

    public GameObject parentGameObject;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();

    }

    void OnMouseDown()
    {
        // Calculate the direction to swipe away
        Debug.Log("tocado");
        Vector2 swipeDirection = -rb.velocity.normalized*100; // Assuming the object is moving, swipe in the opposite direction
        if (swipeDirection == Vector2.zero)
        {
            // If the object wasn't moving, default to swiping it upwards or any other default direction
            swipeDirection = Vector2.up;
        }

        // Apply a force in the calculated direction
        rb.AddForce(swipeDirection * swipeStrength, ForceMode2D.Impulse);
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
        {
            // Check if the script is not the current script (the one removing the others).
            if (script != this)
            {
                // Remove the script component.
                Destroy(script);
            }
        }
         rb.AddForce(swipeDirection * swipeStrength, ForceMode2D.Impulse);
         Destroy(parentGameObject);
    }

    void OnBecameInvisible()
    {
        // Destroy the object when it is no longer visible
        Destroy(gameObject);
    }
}
