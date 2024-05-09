using UnityEngine;

public class GyroOrTouchMove : MonoBehaviour
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
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Immune");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 1.6f;
            rb.AddForce(new Vector2(0, upliftForce*2), ForceMode2D.Impulse);
            StartCoroutine(spawner.Dead());
            audioSource.PlayOneShot(audioClipDeath);
        }
        if(collision.CompareTag("Healing"))
        {
            audioSource.PlayOneShot(success);
            spawner.UpdatePoints();
            Destroy(collision.gameObject);
        }
    }
}
