using UnityEngine;

public class Player3 : MonoBehaviour
{
    public float speed = 10.0f;
    private bool gyroAvailable;
    private bool isDead;
    private Rigidbody2D rb;
    public float upliftForce = 5.0f; 
    public AudioClip success, audioClipDeath;
    private AudioSource audioSource;
    public SpawnerJuego2 spawner;
    public Animator animator;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        gyroAvailable = SystemInfo.supportsGyroscope;
        if (gyroAvailable)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        if (gyroAvailable)
        {
            MoveWithGyro();
        }
        else
        {
            MoveWithTouch();
        }
    }

    void MoveWithGyro()
    {
        Vector3 gyroRotation = Input.gyro.rotationRate;
        transform.Translate(gyroRotation.y * speed * Time.deltaTime, 0, 0);
    }

    void MoveWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z - Camera.main.transform.position.z));
                Vector3 targetPosition = new Vector3(touchPosition.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
{
    // Check collision with "Hitbox"
    if (collision.collider.CompareTag("Hitbox"))
    {
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Immune");
        StartCoroutine(spawner.Dead());
        audioSource.PlayOneShot(audioClipDeath);
    }

    // Check collision with "Obstacle"
    if (collision.collider.CompareTag("Obstacle"))
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical movement
        rb.AddForce(new Vector2(0, upliftForce), ForceMode2D.Impulse); // Apply upward force
    }

    // Check collision with "EnemyObstacle"
    if (collision.collider.CompareTag("EnemyObstacle"))
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical movement
        rb.AddForce(new Vector2(0, -upliftForce / 2), ForceMode2D.Impulse); // Apply downward force
        // audioSource.PlayOneShot(success);
    }
}

}
