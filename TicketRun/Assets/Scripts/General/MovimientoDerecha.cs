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
    private float direction = 1;

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
            DetermineDirection(other);
            targetAlpha = 0.01f;  // Set alpha to low when inside the trigger
            currentSpeed = 0; // Stop the movement by setting speed to 0
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) {
            isColliding = false;
            targetAlpha = 1.0f;  // Restore alpha to full when exiting the trigger
            direction = 1; 
        }
    }
    private void DetermineDirection(Collider2D other)
    {
        direction = transform.position.x >= other.transform.position.x-0.03f ?  1 : -1.5f;
    }
    private void DetermineRotationDirection(Collider2D other)
    {
        Vector2 relativePosition = other.transform.position - transform.position;
        rotationDirection = relativePosition.y >= 0 ? -1 : 1;
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
            spriteColor.a = Mathf.Lerp(spriteColor.a, targetAlpha, Time.deltaTime * 2); // Smooth transition of alpha
            spriteRenderer.color = spriteColor;
        }
    }

    private void UpdateMovement()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.7f);
        rb.velocity = transform.right * currentSpeed * direction;

    }
}
