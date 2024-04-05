using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Tocado : MonoBehaviour
{
    public float swipeStrength = 5f; // Adjust this value to change how far objects are swiped away

    private Rigidbody2D rb;

    public GameObject parentGameObject; // Ensure this is assigned, possibly this gameObject if it's the parent

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if there's any touch input
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began) // Check if the touch just began
                {
                    // Convert touch position to world point
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0; // Ensure the z position is set correctly for a 2D game

                    // Perform a raycast to see if we hit this object
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        HandleTouch();
                    }
                }
            }
        }
    }

    private void HandleTouch()
    {
        Vector2 swipeDirection = -rb.velocity.normalized * 100; // Assuming the object is moving, swipe in the opposite direction
        if (swipeDirection == Vector2.zero)
        {
            swipeDirection = Vector2.up;
        }

        rb.AddForce(swipeDirection * swipeStrength, ForceMode2D.Impulse);

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this)
            {
                Destroy(script);
            }
        }

        rb.AddForce(swipeDirection * swipeStrength, ForceMode2D.Impulse);
        Destroy(parentGameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
