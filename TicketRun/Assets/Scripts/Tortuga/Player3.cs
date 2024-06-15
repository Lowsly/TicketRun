using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Player3 : MonoBehaviour
{
    public float speed = 10.0f;
    private bool gyroAvailable;
    private bool isDead, ready = false;
    private Rigidbody2D rb;
    public float upliftForce = 5.0f; 
    public AudioClip success, audioClipDeath;
    private AudioSource audioSource;

    public Animator animator;
    public GameObject presiona;
    public Background3 background;
    public GameObject gameOver,pauseButton, pauseMenu, optionsMenu;  
    public TextMeshProUGUI time, bestTime;

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
         if (Input.GetMouseButtonDown(0) && !ready && EventSystem.current.currentSelectedGameObject == null)
        {
            ready = true;
            rb.gravityScale = 0.5f;
            animator.SetFloat("speed",1f);
            presiona.SetActive(false);
        }
        if(ready)
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
        if (collision.collider.CompareTag("Hitbox"))
        {
            gameObject.layer = LayerMask.NameToLayer("Immune");
            
            pauseMenu.SetActive(false);
            pauseButton.SetActive(false);
            optionsMenu.SetActive(false);
            gameOver.SetActive(true);     
            int Height = background.totalAscent2;
            PlayerPrefs.SetInt("Height", Height);
            time.text = "Altura actual: " +  Height + " m";
            if(Height > PlayerPrefs.GetInt("BestHeight",0))
            {
                PlayerPrefs.SetInt("BestHeight", Height);
                PlayerPrefs.Save();
                bestTime.text = "Altura máxima lograda: " + Height + " m";
            }
            else
            {
                bestTime.text = "Altura máxima lograda: " +  PlayerPrefs.GetInt("BestHeight",0) + " m";
            }
        }

        if (collision.collider.CompareTag("Obstacle"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical movement
            rb.AddForce(new Vector2(0, upliftForce), ForceMode2D.Impulse); // Apply upward force
        }

        if (collision.collider.CompareTag("EnemyObstacle"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical movement
            rb.AddForce(new Vector2(0, -upliftForce / 2), ForceMode2D.Impulse); // Apply downward force
            // audioSource.PlayOneShot(success);
        }
    }

}
