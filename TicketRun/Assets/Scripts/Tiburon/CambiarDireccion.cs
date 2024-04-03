using UnityEngine;

public class CambiarDireccion : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 1.0f;
    public float directionChangeInterval = 2.0f; // Time in seconds between direction changes
    private Rigidbody2D rb;
    private float timeSinceLastDirectionChange = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Update the timer
        timeSinceLastDirectionChange += Time.deltaTime;

        // Check if it's time to change direction
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            // Reset the timer
            timeSinceLastDirectionChange = 0;

            // Change direction to face the player
            Vector2 directionToPlayer = (playerTransform.position - transform.position);
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust if your sprite's forward direction is different
        }

        // Continue moving in the current direction
        rb.velocity = transform.up * speed;
    }

    // Adjust to set the shark's initial rotation based on the desired direction
    public void SetMoveDirection(float direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }
}
