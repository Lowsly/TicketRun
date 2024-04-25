using UnityEngine;

public class MovimientoDerecha : MonoBehaviour
{
    public float maxSpeed = 0.2f; // The maximum speed
    private float currentSpeed = 0.0f; // Current speed, starts at 0
    private Rigidbody2D rb;
    private bool isColliding = false;
    private float rotationDirection = 0;  // Determines rotation direction based on trigger position
    private float targetAlpha = 1.0f;     // Target alpha value for the sprite
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isColliding) {
            RotateBasedOnCollisionPoint();
        }
        UpdateMovement();
        UpdateAlpha();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) {
            isColliding = true;
            DetermineRotationDirection(other);
            targetAlpha = 0.1f;  // Set alpha to low when inside the trigger
            currentSpeed = 0; // Stop the movement by setting speed to 0
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) {
            isColliding = false;
            targetAlpha = 1.0f;  // Restore alpha to full when exiting the trigger
        }
    }

    private void DetermineRotationDirection(Collider2D other)
    {
        Vector2 relativePosition = other.transform.position - transform.position;
        rotationDirection = relativePosition.y >= 0 ? 1 : -1;
    }

    private void RotateBasedOnCollisionPoint()
    {
        float rotationSpeed = 20f; // Degrees per second
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    private void UpdateAlpha()
    {
        if (spriteRenderer != null) {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = Mathf.Lerp(spriteColor.a, targetAlpha, Time.deltaTime * 5); // Smooth transition of alpha
            spriteRenderer.color = spriteColor;
        }
    }

    private void UpdateMovement()
    {
        if (!isColliding) {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 2); // Gradually increase speed to maxSpeed
        } else {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2); // Gradually decrease speed to 0
        }
        rb.velocity = transform.right * currentSpeed;
    }
}
