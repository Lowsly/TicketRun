using UnityEngine;

public class MovimientoSigiloso : MonoBehaviour
{
    public float speed = 1.0f;
    private float orbitDistance = 1.0f; // Adjust as needed
    public float acceleration = 0.05f; // How quickly the shark accelerates while orbiting
    private float angle; // Keeps track of the orbit angle

    private Rigidbody2D rb;
    private bool isOrbiting = false;
    private float timeToStartOrbiting = 1.0f; // Delay before starting to orbit
    private Vector3 initialDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Calculate initial direction towards (0,0)
        initialDirection = (Vector3.zero - transform.position).normalized;
        Invoke("StartOrbiting", timeToStartOrbiting); // Delay orbit start
    }

    void FixedUpdate()
    {
        if (!isOrbiting)
        {
            // Move in a straight line towards (0,0)
            rb.velocity = initialDirection * speed;
        }
        else
        {
            // Orbit around (0,0) in a spiral pattern
            angle += speed * Time.fixedDeltaTime; // Increase angle to move in a circle
            orbitDistance -= 0.01f * Time.fixedDeltaTime; // Decrease distance to spiral towards (0,0)

            // Calculate the new position for orbiting
            Vector2 orbitPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitDistance;
            Vector2 directionToOrbit = (orbitPosition - (Vector2)transform.position).normalized;

            // Update velocity towards the new position
            rb.velocity = directionToOrbit * speed;

            // Increase speed to accelerate the spiral movement
            speed += acceleration * Time.fixedDeltaTime;
        }
    }

    void StartOrbiting()
    {
        isOrbiting = true;
        rb.velocity = Vector2.zero; // Stop the straight line movement
    }
}
